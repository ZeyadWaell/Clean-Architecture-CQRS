// src/routes/AppRoutes.jsx
import React from 'react'
import { Routes, Route, Navigate } from 'react-router-dom'
import LoginPage from '../features/auth/Login/LoginPage'
import RegisterPage from '../features/auth/Register/RegisterPage'
import ChatPage from '../features/chat/pages/ChatPage/ChatPage'
import ProtectedRoute from '../features/chat/components/ProtectedRoute'
import RoutesClass from '../config/RoutesClass'

function AppRoutes() {
  return (
    <Routes>
      <Route path={RoutesClass.LOGIN} element={<LoginPage />} />
      <Route path={RoutesClass.REGISTER} element={<RegisterPage />} />
      <Route
        path={RoutesClass.CHAT}
        element={
          <ProtectedRoute>
            <ChatPage />
          </ProtectedRoute>
        }
      />
      <Route path={RoutesClass.NOT_FOUND} element={<Navigate to={RoutesClass.LOGIN} replace />} />
    </Routes>
  )
}

export default AppRoutes
