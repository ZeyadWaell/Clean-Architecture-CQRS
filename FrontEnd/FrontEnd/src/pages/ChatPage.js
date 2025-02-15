// src/pages/ChatPage.js
import React, { useState, useEffect, useRef } from 'react';
import API from '../api/api';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import ChatRoomList from '../components/ChatRoomList';
import MessageList from '../components/MessageList';

const ChatPage = () => {
  const [rooms, setRooms] = useState([]);
  const [currentRoom, setCurrentRoom] = useState(null);
  const [messages, setMessages] = useState([]);
  const [messageText, setMessageText] = useState('');
  const [editingMessageId, setEditingMessageId] = useState(null);
  const [editText, setEditText] = useState('');
  const [hubConnected, setHubConnected] = useState(false);
  const hubConnectionRef = useRef(null);

  // 1. Load rooms
  useEffect(() => {
    const fetchRooms = async () => {
      try {
        const { data } = await API.get('/chatRooms/getall');
        setRooms(data.data || []);
      } catch (error) {
        console.error('Error fetching rooms:', error);
      }
    };
    fetchRooms();
  }, []);

  // 2. Load messages
  const fetchMessages = async (roomId) => {
    try {
      const { data } = await API.get(`/chat/messages/${roomId}`);
      setMessages(data.data || []);
    } catch (error) {
      console.error('Error fetching messages:', error);
    }
  };

  // 3. Join room
  const joinRoom = async (room) => {
    try {
      setCurrentRoom(room);
      const token = localStorage.getItem('token');

      const connection = new HubConnectionBuilder()
        .withUrl('https://localhost:44307/chathub', {
          accessTokenFactory: () => token,
        })
        .configureLogging(LogLevel.Information)
        .withAutomaticReconnect()
        .build();

      connection.on('ReceiveMessage', (message) => {
        setMessages((prev) => [...prev, message]);
      });
      connection.on('MessageEdited', (editedMessage) => {
        setMessages((prev) =>
          prev.map((msg) =>
            msg.messageId === editedMessage.messageId ? editedMessage : msg
          )
        );
      });
      connection.on('MessageDeleted', (deletedMessageId) => {
        setMessages((prev) =>
          prev.filter((msg) => msg.messageId !== deletedMessageId)
        );
      });

      await connection.start();
      await connection.invoke('JoinRoom', room.id);
      hubConnectionRef.current = connection;
      setHubConnected(true);

      // fetch existing messages
      fetchMessages(room.id);
    } catch (error) {
      console.error('Error joining room:', error);
    }
  };

  // 4. Leave room
  const leaveRoom = async () => {
    if (!currentRoom) return;
    try {
      await API.delete('/chatRooms/leave', {
        data: { chatRoomId: currentRoom.id },
      });
      if (hubConnectionRef.current) {
        await hubConnectionRef.current.invoke('LeaveRoom', currentRoom.id);
        await hubConnectionRef.current.stop();
      }
      setCurrentRoom(null);
      setMessages([]);
      setHubConnected(false);
    } catch (error) {
      console.error('Error leaving room:', error);
    }
  };

  // 5. Send message
  const handleSendMessage = async () => {
    if (!currentRoom || !hubConnected || !messageText.trim()) return;
    try {
      await hubConnectionRef.current.invoke('SendMessage', {
        chatRoomId: currentRoom.id,
        message: messageText,
      });
      setMessageText('');
    } catch (error) {
      console.error('Error sending message:', error);
    }
  };

  // 6. Edit message
  const handleEditMessage = async (msgId) => {
    if (!currentRoom || !hubConnected) return;
    try {
      await hubConnectionRef.current.invoke('EditMessage', {
        messageId: msgId,
        chatRoomId: currentRoom.id,
        newContent: editText,
      });
      setEditingMessageId(null);
      setEditText('');
    } catch (error) {
      console.error('Error editing message:', error);
    }
  };

  // 7. Delete message
  const handleDeleteMessage = async (msgId) => {
    if (!currentRoom || !hubConnected) return;
    try {
      await hubConnectionRef.current.invoke('DeleteMessage', {
        messageId: msgId,
        chatRoomId: currentRoom.id,
      });
    } catch (error) {
      console.error('Error deleting message:', error);
    }
  };

  return (
    <div className="container-fluid vh-100 d-flex flex-column">
      {/* Simple topbar */}
      <div className="row bg-dark text-white align-items-center p-2">
        <div className="col">
          <h4 className="mb-0">My ChatApp</h4>
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
        {/* Left column: rooms */}
        <div className="col-3 bg-light p-0 border-end" style={{ overflowY: 'auto' }}>
          <ChatRoomList rooms={rooms} onJoin={joinRoom} />
        </div>

        {/* Right column: messages */}
        <div className="col-9 d-flex flex-column p-0">
          {currentRoom ? (
            <>
              {/* Messages */}
              <div className="flex-grow-1" style={{ overflowY: 'auto' }}>
                <MessageList
                  messages={messages}
                  editingMessageId={editingMessageId}
                  onEditInit={setEditingMessageId}
                  editText={editText}
                  setEditText={setEditText}
                  onEditSave={handleEditMessage}
                  onDelete={handleDeleteMessage}
                />
              </div>
              {/* Message input */}
              <div className="p-2 border-top">
                <div className="input-group">
                  <input
                    className="form-control"
                    type="text"
                    placeholder="Type a message..."
                    value={messageText}
                    onChange={(e) => setMessageText(e.target.value)}
                    onKeyPress={(e) => {
                      if (e.key === 'Enter') {
                        handleSendMessage();
                      }
                    }}
                  />
                  <button
                    className="btn btn-primary"
                    onClick={handleSendMessage}
                  >
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
  );
};

export default ChatPage;
