import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import apiClient from '../../../api/apiClient'

import styles from './RegisterPage.module.css'

function RegisterPage() {
  const [formData, setFormData] = useState({
    email: '',
    userName: '',
    password: '',
    nikName: '',
  })
  const [error, setError] = useState('')
  const navigate = useNavigate()

  const handleChange = (e) => {
    setFormData((prev) => ({ ...prev, [e.target.name]: e.target.value }))
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      await apiClient.post('/account/register', formData)
      navigate('/login')
    } catch {
      setError('Registration failed. Please try again.')
    }
  }

  return (
    <div className={styles.registerPage}>
      <div className={styles.formCard}>
        <h2 className={styles.title}>Register</h2>
        {error && <div className={styles.error}>{error}</div>}
        <form onSubmit={handleSubmit}>
          <div className={styles.field}>
            <label>Email</label>
            <input
              type="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              required
            />
          </div>
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
          <div className={styles.field}>
            <label>Nick Name</label>
            <input
              type="text"
              name="nikName"
              value={formData.nikName}
              onChange={handleChange}
            />
          </div>
          <button type="submit" className={styles.submitButton}>Register</button>
        </form>
        <p className={styles.switch}>
          Already have an account? <a href="/login">Login here</a>
        </p>
      </div>
    </div>
  )
}

export default RegisterPage
