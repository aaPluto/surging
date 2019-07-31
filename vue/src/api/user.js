import axios from '@/libs/api.request'
import config from '@/config'
const prefix = config.prefix

export const getUserInfo = (token) => {
  return axios.request({
    url: prefix + 'getuserinfo',
    params: {
      token
    },
    method: 'get'
  })
}

export const login = ({ userName, password }) => {
  const data = {
    "input": {
      userName,
      password
    }
  }
  debugger
  return axios.request({
    url: prefix + 'account/login',
    data,
    method: 'post'
  })
}
