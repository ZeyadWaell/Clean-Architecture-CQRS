import React, { Component } from 'react'
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom'
import ProtectedRoute from './components/ProtectedRoute'
import LoginPage from './pages/LoginPage'
import RegisterPage from './pages/RegisterPage'
import ChatPage from './pages/ChatPage'
import RoutesClass from './config/RoutesClass'

class App extends Component {
  render() {
    return (
      <Router>
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
      </Router>
    )
  }
}

export default App
