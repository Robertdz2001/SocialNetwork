import React, { useEffect, useState, useRef } from 'react';
import axios from 'axios';
import { HubConnectionBuilder, HttpTransportType } from '@microsoft/signalr';
import { baseUrl, formatDate } from '../Shared/Options/ApiOptions';
import classes from './ChatPage.module.scss';
import fonts from '../Shared/fonts.module.scss';
import icons from '../Shared/icons.module.scss';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCalendarDays } from '@fortawesome/free-solid-svg-icons';

const ChatPage = () => {
  const [chats, setChats] = useState([]);
  const [filteredChats, setFilteredChats] = useState([]);
  const [messages, setMessages] = useState([]);
  const [selectedChatId, setSelectedChatId] = useState(null);
  const [searchTerm, setSearchTerm] = useState("");
  const [newMessage, setNewMessage] = useState("");
  const [connection, setConnection] = useState(null);
  const messagesEndRef = useRef(null);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView();
  };

  useEffect(() => {
    const connectSignalR = async () => {
      const conn = new HubConnectionBuilder()
        .withUrl('https://localhost:7229/chat', {
          skipNegotiation: true,
          transport: HttpTransportType.WebSockets,
        })
        .withAutomaticReconnect()
        .build();

      conn.on("SendMessageToChat", (chatId) => {
        if (chatId === selectedChatId) {
          fetchMessages(chatId);
        }
      });

      await conn.start();
      setConnection(conn);
    };

    connectSignalR();
  }, [selectedChatId]);

  useEffect(() => {
    if (connection && selectedChatId) {
      connection.invoke("JoinChat", selectedChatId).catch((err) => console.error(err));
      return () => {
        connection.invoke("LeaveChat", selectedChatId).catch((err) => console.error(err));
      };
    }
  }, [connection, selectedChatId]);

  useEffect(() => {
    const fetchChats = async () => {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.get(`${baseUrl}/chat`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        setChats(response.data);
        setFilteredChats(response.data);
      } catch (error) {
        console.error("Error fetching chats:", error);
      }
    };
    fetchChats();
  }, []);

  const fetchMessages = async (chatId) => {
    try {
      const token = localStorage.getItem('token');
      const response = await axios.get(`${baseUrl}/chat/${chatId}/messages`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      setMessages(response.data);
      setSelectedChatId(chatId);
    } catch (error) {
      console.error("Error fetching messages:", error);
    }
  };

  const sendMessage = async () => {
    if (!newMessage.trim()) return;
    try {
      const token = localStorage.getItem('token');
      await axios.post(
        `${baseUrl}/chat/${selectedChatId}/messages`,
        JSON.stringify(newMessage),
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      setNewMessage("");
    } catch (error) {
      console.error("Error sending message:", error);
    }
  };

  useEffect(() => {
    scrollToBottom();
  }, [messages]);

  useEffect(() => {
    const filtered = chats.filter((chat) =>
      `${chat.userFirstName} ${chat.userLastName}`
        .toLowerCase()
        .includes(searchTerm.toLowerCase())
    );
    setFilteredChats(filtered);
  }, [searchTerm, chats]);

  return (
    <div className='d-flex h-100'>
      <div className={classes['chats-container']}>
        <input
          type="text"
          placeholder="Search..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className={classes['chats-input']}
        />
        <div>
          {filteredChats.map((chat) => (
            <div
              key={chat.id}
              onClick={() => fetchMessages(chat.id)}
              className={`${classes["chat-item"]} ${selectedChatId === chat.id ? classes["chat-item-active"] : ""
                } d-flex align-items-center mb-5`}
            >
              <div>
                <img
                  src={`${baseUrl}/user/${chat.userId}/profile-picture`}
                  alt="Profile"
                  className={classes['chat-user-profile-picture']} />
              </div>
              <div className='ms-3'>
                <div className={fonts["font-green-small"]}>
                  {chat.userFirstName} {chat.userLastName}
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
      {
        selectedChatId && (
          <div className={classes['messages-container']}>
            <div className={classes['messages-container-content']}>
              {messages.map((message, index) => (
                <div
                  key={index}
                  className={`${classes['message-item']} mb-3 ${message.isSentByLoggedUser ? "align-self-end" : ""}`}
                >
                  <div className='d-flex align-items-center justify-content-between mb-2 me-3'>
                    <div className='d-flex align-items-center'>
                      <div className='d-flex align-items-center'>
                        <div className='ms-2'>
                          <img
                            src={`${baseUrl}/user/${message.userId}/profile-picture`}
                            alt="Profile"
                            className={`${classes['chat-user-profile-picture']} me-2`} />
                        </div>
                        <div className={fonts["font-green-small"]}>
                          <FontAwesomeIcon icon={faCalendarDays} className={icons.icon} /> {formatDate(message.createdDate)}
                        </div>
                      </div>
                    </div>
                  </div>
                  <div className={`${fonts["font-green-small"]} text-break`}>
                    {message.content}
                  </div>
                </div>
              ))}
              <div ref={messagesEndRef} />
            </div>
            <div className={classes['send-container']}>
              <input
                type="text"
                placeholder="Enter Message"
                value={newMessage}
                onChange={(e) => setNewMessage(e.target.value)}
                className={`${classes['chats-input']} m-0 me-3`}
              />
              <button onClick={sendMessage} className={classes['send-button']}>Send</button>
            </div>
          </div>
        )
      }
    </div >
  );
};

export default ChatPage;
