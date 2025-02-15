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
    <div className="card shadow d-flex flex-column message-list-container">
      <div className="card-header bg-secondary text-white">
        <h6 className="mb-0">Messages</h6>
      </div>
      <div className="messages-body">
        {messages.length === 0 ? (
          <div className="alert alert-info">No messages in this room.</div>
        ) : (
          messages.map((msg) => {
            const isMine = msg.sender === currentUserName
            const isEditing = editingMessageId === msg.messageId
            return (
              <div
                key={msg.messageId}
                className={`message-bubble ${isMine ? 'mine' : 'theirs'}`}
              >
                {!isMine && (
                  <div className="sender-name">{msg.sender}</div>
                )}
                {isEditing ? (
                  <div className="edit-container">
                    <input
                      type="text"
                      value={editText}
                      onChange={(e) => setEditText(e.target.value)}
                      className="form-control mb-2"
                      placeholder="Edit your message"
                    />
                    <div className="d-flex justify-content-end">
                      <button
                        className="btn btn-success btn-sm me-2"
                        onClick={() => onEditSave(msg.messageId)}
                      >
                        Save
                      </button>
                      <button
                        className="btn btn-secondary btn-sm"
                        onClick={() => onEditInit(null)}
                      >
                        Cancel
                      </button>
                    </div>
                  </div>
                ) : (
                  <>
                    <div className="message-text">{msg.message}</div>
                    {isMine && (
                      <div className="message-actions text-end mt-1">
                        <button
                          className="btn btn-link p-0 me-2 text-white text-decoration-none"
                          onClick={() => onEditInit(msg.messageId)}
                        >
                          Edit
                        </button>
                        <button
                          className="btn btn-link p-0 text-danger text-decoration-none"
                          onClick={() => onDelete(msg.messageId)}
                        >
                          Delete
                        </button>
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
