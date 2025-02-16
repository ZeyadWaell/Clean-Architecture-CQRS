// C:\Users\pc\source\repos\ChatApp\ProjectRoot\FrontEnd\FrontEnd\src\features\chat\pages\ChatPage\ChatPage.jsx
import React, { useState, useEffect, useRef } from 'react'
import { useNavigate } from 'react-router-dom'
import apiClient from '../../../../api/apiClient'
import ChatRoomList from '../../components/ChatRoomList/ChatRoomList'
import MessageList from '../../components/MessageList/MessageList'
import styles from './ChatPage.module.css'
import RoutesClass from '../../../../config/RoutesClass'
import ApiConfig from '../../../../config/ApiConfig'

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
  const navigate = useNavigate()

  useEffect(() => {
    const fetchRooms = async () => {
      try {
        const { data } = await apiClient.get(RoutesClass.GET_ALL_CHATROOMS)
        setRooms(data.data || [])
      } catch (error) {}
    }
    fetchRooms()
  }, [])

  const fetchMessages = async (roomId) => {
    try {
      const { data } = await apiClient.get(`${RoutesClass.MESSAGES}/${roomId}`)
      setMessages(data.data || [])
    } catch (error) {}
  }

  const joinRoom = async (room) => {
    try {
      setCurrentRoom(room)
      const token = localStorage.getItem('token')
      const { HubConnectionBuilder, LogLevel } = await import('@microsoft/signalr')
      const connection = new HubConnectionBuilder()
        .withUrl(`${ApiConfig.MAIN_URL}${RoutesClass.CHATHUB}`, { accessTokenFactory: () => token })
        .configureLogging(LogLevel.Information)
        .withAutomaticReconnect()
        .build()
      connection.on('ReceiveMessage', (msg) => {
        setMessages((prev) => [...prev, msg])
      })
      connection.on('MessageEdited', (edited) => {
        setMessages((prev) => prev.map((m) => (m.messageId === edited.messageId ? edited : m)))
      })
      connection.on('MessageDeleted', (delId) => {
        setMessages((prev) => prev.filter((m) => m.messageId !== delId))
      })
      connection.on('UserJoined', (userId) => {
        setMessages((prev) => [
          ...prev,
          { messageId: `sys-${Date.now()}`, sender: 'System', message: `${userId} has joined the chat` }
        ])
      })
      connection.on('UserLeft', (userId) => {
        setMessages((prev) => [
          ...prev,
          { messageId: `sys-${Date.now()}`, sender: 'System', message: `${userId} left the chat` }
        ])
      })
      await connection.start()
      await connection.invoke('JoinRoom', room.id)
      hubConnectionRef.current = connection
      setHubConnected(true)
      fetchMessages(room.id)
    } catch (error) {}
  }

  const leaveRoom = async () => {
    if (!currentRoom) return
    try {
      await hubConnectionRef.current.invoke('LeaveRoom', currentRoom.id)
      await hubConnectionRef.current.stop()
      setCurrentRoom(null)
      setMessages([])
      setHubConnected(false)
      navigate(RoutesClass.CHAT)
    } catch (error) {}
  }

  const handleSendMessage = async () => {
    if (!currentRoom || !hubConnected || !messageText.trim()) return
    try {
      await hubConnectionRef.current.invoke('SendMessage', {
        chatRoomId: currentRoom.id,
        message: messageText
      })
      setMessageText('')
    } catch (error) {}
  }

  const handleEditMessage = async (id) => {
    if (!currentRoom || !hubConnected) return
    try {
      await hubConnectionRef.current.invoke('EditMessage', {
        messageId: id,
        chatRoomId: currentRoom.id,
        newContent: editText
      })
      setEditingMessageId(null)
      setEditText('')
    } catch (error) {}
  }

  const handleDeleteMessage = async (id) => {
    if (!currentRoom || !hubConnected) return
    try {
      await hubConnectionRef.current.invoke('DeleteMessage', {
        messageId: id,
        chatRoomId: currentRoom.id
      })
    } catch (error) {}
  }

  return (
    <div className={styles.chatPage}>
      <div className={styles.header}>
        <h5>My Chat App</h5>
        {currentRoom && (
          <button className={styles.leaveBtn} onClick={leaveRoom}>
            Leave {currentRoom.name}
          </button>
        )}
      </div>
      <div className={styles.mainContent}>
        <div className={styles.roomList}>
          <ChatRoomList rooms={rooms} onJoin={joinRoom} />
        </div>
        <div className={styles.chatContainer}>
          {currentRoom ? (
            <>
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
              <div className={styles.messageInput}>
                <input
                  type="text"
                  placeholder="Type a message..."
                  value={messageText}
                  onChange={(e) => setMessageText(e.target.value)}
                  onKeyDown={(e) => {
                    if (e.key === 'Enter') handleSendMessage()
                  }}
                />
                <button onClick={handleSendMessage}>Send</button>
              </div>
            </>
          ) : (
            <div className={styles.noRoom}>
              <h5>Select a room to start chatting.</h5>
            </div>
          )}
        </div>
      </div>
    </div>
  )
}

export default ChatPage
