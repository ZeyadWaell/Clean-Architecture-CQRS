// src/features/chat/components/ChatRoomList/ChatRoomList.jsx
import React from 'react'
import styles from './ChatRoomList.module.css' // CSS Module

function ChatRoomList({ rooms, onJoin }) {
  return (
    <div className={`card ${styles.roomListContainer}`}>
      <div className={`card-header ${styles.cardHeader}`}>
        <h5>Chat Rooms</h5>
      </div>
      <div className={`card-body ${styles.cardBody}`}>
        {rooms.length === 0 ? (
          <div className={styles.emptyList}>No chat rooms available.</div>
        ) : (
          <ul className={styles.listGroup}>
            {rooms.map((room) => (
              <li
                key={room.id}
                className={styles.listGroupItem}
                onClick={() => onJoin(room)}
              >
                <span className={styles.roomName}>{room.name}</span>
                <span className={styles.joinText}>Join</span>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  )
}

export default ChatRoomList
