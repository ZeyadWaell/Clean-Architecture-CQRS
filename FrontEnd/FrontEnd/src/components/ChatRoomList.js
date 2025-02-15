import React from 'react'
import './ChatRoomList.css'

function ChatRoomList({ rooms, onJoin }) {
  return (
    <div className="room-list-container card">
      <div className="card-header">
        <h5>Chat Rooms</h5>
      </div>
      <div className="card-body">
        {rooms.length === 0 ? (
          <div className="empty-list">No chat rooms available.</div>
        ) : (
          <ul className="list-group">
            {rooms.map((room) => (
              <li
                key={room.id}
                className="list-group-item"
                onClick={() => onJoin(room)}
              >
                <span className="room-name">{room.name}</span>
                <span className="join-text">Join</span>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  )
}

export default ChatRoomList
