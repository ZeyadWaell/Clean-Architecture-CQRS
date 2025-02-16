import React, { useEffect, useRef } from 'react'
import { motion, AnimatePresence } from 'framer-motion'
import styles from './MessageList.module.css'

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
  const containerRef = useRef(null)
  useEffect(() => {
    if (containerRef.current) {
      containerRef.current.scrollTop = containerRef.current.scrollHeight
    }
  }, [messages])
  return (
    <div className={`card ${styles.messageListContainer}`}>
      <div className={`card-header ${styles.cardHeader}`}>
        <h6>Messages</h6>
      </div>
      <div className={styles.messagesBody} ref={containerRef}>
        {messages.length === 0 ? (
          <div className={styles.noMessages}>No messages in this room.</div>
        ) : (
          <AnimatePresence>
            {messages.map((msg) => {
              const isMine = msg.sender === currentUserName
              const isEditing = editingMessageId === msg.messageId
              return (
                <motion.div
                  key={msg.messageId}
                  className={`${styles.messageBubble} ${isMine ? styles.mine : styles.theirs}`}
                  initial={{ opacity: 0, y: 15 }}
                  animate={{ opacity: 1, y: 0 }}
                  exit={{ opacity: 0, y: -15 }}
                  layout
                >
                  {!isMine && <div className={styles.senderName}>{msg.sender}</div>}
                  {isEditing ? (
                    <div className={styles.editContainer}>
                      <input
                        type="text"
                        value={editText}
                        onChange={(e) => setEditText(e.target.value)}
                        placeholder="Edit your message"
                      />
                      <div className={styles.editActions}>
                        <button onClick={() => onEditSave(msg.messageId)}>Save</button>
                        <button onClick={() => onEditInit(null)}>Cancel</button>
                      </div>
                    </div>
                  ) : (
                    <>
                      <div className={styles.messageText}>{msg.message}</div>
                      {isMine && (
                        <div className={styles.messageActions}>
                          <button onClick={() => onEditInit(msg.messageId)}>Edit</button>
                          <button onClick={() => onDelete(msg.messageId)}>Delete</button>
                        </div>
                      )}
                    </>
                  )}
                </motion.div>
              )
            })}
          </AnimatePresence>
        )}
      </div>
    </div>
  )
}

export default MessageList
