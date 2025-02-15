import React, { useState, useEffect, useRef } from 'react'
import API from '../api/API'
import ChatRoomList from '../components/ChatRoomList'
import MessageList from '../components/MessageList'

function ChatPage() {
  const [rooms, setRooms] = useState([])
  const [currentRoom, setCurrentRoom] = useState(null)
  const [messages, setMessages] = useState([])
  const [messageText, setMessageText] = useState('')
  const [editingMessageId, setEditingMessageId] = useState(null)
  const [editText, setEditText] = useState('')
  const [hubConnected, setHubConnected] = useState(false)
  const hubConnectionRef = useRef(null)
  const currentUserName = localStorage.getItem('username') || 'Me'

  useEffect(() => {
    const fetchRooms = async () => {
      try {
        const { data } = await API.get('/chatRooms/getall')
        setRooms(data.data || [])
      } catch {}
    }
    fetchRooms()
  }, [])

  const fetchMessages = async (roomId) => {
    try {
      const { data } = await API.get(`/chat/messages/${roomId}`)
      setMessages(data.data || [])
    } catch {}
  }

  const joinRoom = async (room) => {
    try {
      setCurrentRoom(room)
      const token = localStorage.getItem('token')
      const { HubConnectionBuilder, LogLevel } = await import('@microsoft/signalr')
      const connection = new HubConnectionBuilder()
        .withUrl('https://localhost:44307/chathub', { accessTokenFactory: () => token })
        .configureLogging(LogLevel.Information)
        .withAutomaticReconnect()
        .build()
      connection.on('ReceiveMessage', (msg) => {
        setMessages((prev) => [...prev, msg])
      })
      connection.on('MessageEdited', (edited) => {
        setMessages((prev) =>
          prev.map((m) => (m.messageId === edited.messageId ? edited : m))
        )
      })
      connection.on('MessageDeleted', (delId) => {
        setMessages((prev) =>
          prev.filter((m) => m.messageId !== delId)
        )
      })
      await connection.start()
      await connection.invoke('JoinRoom', room.id)
      hubConnectionRef.current = connection
      setHubConnected(true)
      fetchMessages(room.id)
    } catch {}
  }

  const leaveRoom = async () => {
    if (!currentRoom) return
    try {
      await API.delete('/chatRooms/leave', {
        data: { chatRoomId: currentRoom.id },
      })
      if (hubConnectionRef.current) {
        await hubConnectionRef.current.invoke('LeaveRoom', currentRoom.id)
        await hubConnectionRef.current.stop()
      }
      setCurrentRoom(null)
      setMessages([])
      setHubConnected(false)
    } catch {}
  }

  const handleSendMessage = async () => {
    if (!currentRoom || !hubConnected || !messageText.trim()) return
    try {
      await hubConnectionRef.current.invoke('SendMessage', {
        chatRoomId: currentRoom.id,
        message: messageText,
      })
      setMessageText('')
    } catch {}
  }

  const handleEditMessage = async (id) => {
    if (!currentRoom || !hubConnected) return
    try {
      await hubConnectionRef.current.invoke('EditMessage', {
        messageId: id,
        chatRoomId: currentRoom.id,
        newContent: editText,
      })
      setEditingMessageId(null)
      setEditText('')
    } catch {}
  }

  const handleDeleteMessage = async (id) => {
    if (!currentRoom || !hubConnected) return
    try {
      await hubConnectionRef.current.invoke('DeleteMessage', {
        messageId: id,
        chatRoomId: currentRoom.id,
      })
    } catch {}
  }

  return (
    <div className="container-fluid vh-100 d-flex flex-column">
      <div className="row bg-dark text-white align-items-center p-2">
        <div className="col">
          <h5 className="mb-0">My Chat App</h5>
        </div>
        <div className="col-auto">
          {currentRoom && (
            <button className="btn btn-outline-light btn-sm" onClick={leaveRoom}>
              Leave {currentRoom.name}
            </button>
          )}
        </div>
      </div>
      <div className="row flex-grow-1">
        <div className="col-3 bg-light p-0 border-end">
          <ChatRoomList rooms={rooms} onJoin={joinRoom} />
        </div>
        <div className="col-9 d-flex flex-column p-0">
          {currentRoom ? (
            <>
              <div className="flex-grow-1" style={{ overflowY: 'auto' }}>
                <MessageList
                  messages={messages}
                  currentUserName={currentUserName}
                  editingMessageId={editingMessageId}
                  editText={editText}
                  setEditText={setEditText}
                  onEditInit={setEditingMessageId}
                  onEditSave={handleEditMessage}
                  onDelete={handleDeleteMessage}
                />
              </div>
              <div className="p-2 border-top">
                <div className="input-group">
                  <input
                    className="form-control"
                    type="text"
                    placeholder="Type a message..."
                    value={messageText}
                    onChange={(e) => setMessageText(e.target.value)}
                    onKeyPress={(e) => {
                      if (e.key === 'Enter') handleSendMessage()
                    }}
                  />
                  <button className="btn btn-primary" onClick={handleSendMessage}>
                    Send
                  </button>
                </div>
              </div>
            </>
          ) : (
            <div className="d-flex align-items-center justify-content-center h-100">
              <h5 className="text-muted">Select a room to start chatting.</h5>
            </div>
          )}
        </div>
      </div>
    </div>
  )
}

export default ChatPage
