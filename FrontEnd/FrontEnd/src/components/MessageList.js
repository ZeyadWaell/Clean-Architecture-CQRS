import React from 'react'
import './MessageList.css'

function MessageList({
  messages,
  currentUserName,
  editingMessageId,
  editText,
  setEditText,
  onEditInit,
  onEditSave,
  onDelete
}) {
  return (
    <div className="message-list-container card">
      <div className="card-header">
        <h6>Messages</h6>
      </div>
      <div className="messages-body">
        {messages.length === 0 ? (
          <div className="no-messages">No messages in this room.</div>
        ) : (
          messages.map((msg) => {
            const isMine = msg.sender === currentUserName
            const isEditing = editingMessageId === msg.messageId
            return (
              <div
                key={msg.messageId}
                className={`message-bubble ${isMine ? 'mine' : 'theirs'}`}
              >
                {!isMine && <div className="sender-name">{msg.sender}</div>}
                {isEditing ? (
                  <div className="edit-container">
                    <input
                      type="text"
                      value={editText}
                      onChange={(e) => setEditText(e.target.value)}
                      placeholder="Edit your message"
                    />
                    <div className="edit-actions">
                      <button onClick={() => onEditSave(msg.messageId)}>Save</button>
                      <button onClick={() => onEditInit(null)}>Cancel</button>
                    </div>
                  </div>
                ) : (
                  <>
                    <div className="message-text">{msg.message}</div>
                    {isMine && (
                      <div className="message-actions">
                        <button onClick={() => onEditInit(msg.messageId)}>Edit</button>
                        <button onClick={() => onDelete(msg.messageId)}>Delete</button>
                      </div>
                    )}
                  </>
                )}
              </div>
            )
          })
        )}
      </div>
    </div>
  )
}

export default MessageList
