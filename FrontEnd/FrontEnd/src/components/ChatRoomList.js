import React from 'react'
import './ChatRoomList.css'

function ChatRoomList({ rooms, onJoin }) {
  return (
    <div className="room-list-container card shadow h-100">
      <div className="card-header bg-primary text-white">
        <h5 className="mb-0">Chat Rooms</h5>
      </div>
      <div className="card-body p-0">
        {rooms.length === 0 ? (
          <div className="p-3 text-center text-muted">No chat rooms available.</div>
        ) : (
          <ul className="list-group list-group-flush">
            {rooms.map((room) => (
              <li
                key={room.id}
                className="list-group-item d-flex justify-content-between align-items-center"
                style={{ cursor: 'pointer' }}
                onClick={() => onJoin(room)}
              >
                <span className="fw-semibold">{room.name}</span>
                <span className="text-muted small">Join</span>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  )
}

export default ChatRoomList
