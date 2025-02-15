// src/components/MessageList.js
import React from 'react';
import './MessageList.css';

const MessageList = ({
  messages,
  currentUserName,
  editingMessageId,
  editText,
  setEditText,
  onEditInit,
  onEditSave,
  onDelete,
}) => {
  return (
    <div className="message-list-container card shadow d-flex flex-column">
      <div className="card-header bg-secondary text-white">
        <h6 className="mb-0">Messages</h6>
      </div>
      <div className="card-body messages-wrapper">
        {messages.length === 0 ? (
          <div className="alert alert-info">No messages in this room.</div>
        ) : (
          messages.map((msg) => {
            const isMine = msg.sender === currentUserName;
            const isEditing = editingMessageId === msg.messageId;

            return (
              <div
                key={msg.messageId}
                className={`message-bubble ${isMine ? 'mine' : 'theirs'}`}
              >
                {/* Show the sender name if not me (optional, or show for everyone if you prefer) */}
                {!isMine && (
                  <div className="sender-name text-muted mb-1">
                    {msg.sender}
                  </div>
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
                    {/* If it's mine, show Edit/Delete. If you want "Delete" for all, move below. */}
                    {isMine && (
                      <div className="message-actions text-end mt-1">
                        <button
                          className="btn btn-link p-0 me-3 text-white text-decoration-none"
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
                    {/* If you want others to see Delete too, place a button here if !isMine */}
                  </>
                )}
              </div>
            );
          })
        )}
      </div>
    </div>
  );
};

export default MessageList;
