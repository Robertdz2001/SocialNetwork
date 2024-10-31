import React, { useState } from "react";
import classes from './HomePage.module.scss';
import authClasses from '../Authentication/AuthenticationPage.module.scss';
import { baseUrl, authorization } from "../Shared/Options/ApiOptions";
import axios from "axios";

const HomePage = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [content, setContent] = useState("");
  const [photo, setPhoto] = useState(null);

  const toggleModal = () => {
    setIsModalOpen(!isModalOpen);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append("Content", content);
    if (photo) formData.append("Photo", photo);

    try {
      var token = localStorage.getItem('token');
      await axios.post(`${baseUrl}/post`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
            Authorization: `Bearer ${token}`,
        },
    });

      alert("Post created!");
      setContent("");
      setPhoto(null);
      toggleModal();
    } catch (error) {
      alert(error.response.data);
    }
  };

  return (
    <div>
      <button onClick={toggleModal}>Create Post</button>
      {isModalOpen && (
        <div className={classes['modal']}>
          <div className={classes['modal-content']}>
            <h2 className={authClasses['auth-header']}>Create Post</h2>
            <form className={classes['post-form']} onSubmit={handleSubmit}>
              <div className="mb-5">
                <input
                  type="file"
                  accept="image/*"
                  lang="en"
                  className={authClasses['auth-input']}
                  onChange={(e) => setPhoto(e.target.files[0])}
                />
              </div>
              <div>
                <textarea
                  className={`${classes['post-content']} ${authClasses['auth-input']}`}
                  value={content}
                  onChange={(e) => setContent(e.target.value)}
                  required
                  placeholder="Content"
                />
              </div>
              <div className='row justify-content-between'>
                <div className='col-5'>
                  <button onClick={toggleModal} className={authClasses['auth-primary-btn']}>
                    Cancel
                  </button>
                </div>
                <div className='col-5'>
                  <button type='submit' className={authClasses['auth-primary-btn']}>
                    Create
                  </button>
                </div>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}

export default HomePage;