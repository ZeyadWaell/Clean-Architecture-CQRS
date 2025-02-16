import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import apiClient from '../../../api/apiClient'
import styles from './LoginPage.module.css'

function LoginPage() {
  const [formData, setFormData] = useState({ userName: '', password: '' })
  const [error, setError] = useState('')
  const navigate = useNavigate()

  const handleChange = (e) => {
    setFormData((prev) => ({ ...prev, [e.target.name]: e.target.value }))
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      const { data } = await apiClient.post('/account/login', formData)
      if (data.success && data.data?.token) {
        localStorage.setItem('token', data.data.token)
        if (data.data.userName) localStorage.setItem('username', data.data.userName)
        navigate('/chat')
      } else {
        setError(data.message || 'Login failed.')
      }
    } catch {
      setError('Login failed. Please check your credentials.')
    }
  }

  return (
    <div className={styles.loginPage}>
      <div className={styles.formCard}>
        <h2 className={styles.title}>Login</h2>
        {error && <div className={styles.error}>{error}</div>}
        <form onSubmit={handleSubmit}>
          <div className={styles.field}>
            <label>User Name</label>
            <input
              type="text"
              name="userName"
              value={formData.userName}
              onChange={handleChange}
              required
            />
          </div>
          <div className={styles.field}>
            <label>Password</label>
            <input
              type="password"
              name="password"
              value={formData.password}
              onChange={handleChange}
              required
            />
          </div>
          <button type="submit" className={styles.submitButton}>Login</button>
        </form>
        <p className={styles.switch}>
          Don't have an account? <a href="/register">Register here</a>
        </p>
      </div>
    </div>
  )
}

export default LoginPage
