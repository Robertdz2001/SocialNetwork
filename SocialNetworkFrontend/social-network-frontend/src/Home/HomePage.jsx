import React, { useState, useEffect } from "react";
import classes from './HomePage.module.scss';
import authClasses from '../Authentication/AuthenticationPage.module.scss';
import { baseUrl } from "../Shared/Options/ApiOptions";
import axios from "axios";
import ReactPaginate from 'react-paginate';
import paginationClasses from '../Shared/pagination.module.scss';
import PostComponent from "../Shared/Components/PostComponent/PostComponent";
import modalClasses from "../Shared/modals.module.scss";

const HomePage = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [content, setContent] = useState("");
  const [photo, setPhoto] = useState(null);
  const [posts, setPosts] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [triggerEffect, setTriggerEffect] = useState(false);
  const [filters, setFilters] = useState({
    createdUserFirstName: '',
    createdUserLastName: '',
    content: ''
  });

  const fetchPosts = async (currentPage, searchFilters) => {
    try {
      const token = localStorage.getItem('token');

      const params = {
        CreatedUserFirstName: searchFilters.createdUserFirstName,
        CreatedUserLastName: searchFilters.createdUserLastName,
        Content: searchFilters.content,
        PageNumber: currentPage
      };

      const response = await axios.get(`${baseUrl}/post`, {
        params,
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      setPosts(response.data.items);
      setTotalPages(response.data.totalPages);
    } catch (error) {
      console.error(error.message);
    }
  };

  useEffect(() => {
    fetchPosts(pageNumber, filters);
  }, [pageNumber, filters, triggerEffect]);

  const handleSearch = (e) => {
    e.preventDefault();

    const newFilters = {
      createdUserFirstName: e.target.createdUserFirstName.value,
      createdUserLastName: e.target.createdUserLastName.value,
      content: e.target.content.value
    };

    setFilters(newFilters);
    setPageNumber(1);
  };

  const handlePostRefresh = () => {
    setTriggerEffect(prev => !prev);
  };

  const handlePageChange = ({ selected }) => {
    const newPageNumber = selected + 1;
    setPageNumber(newPageNumber);
  };

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
      <form onSubmit={handleSearch} className='d-flex justify-content-center'>
        <div className={classes["filter-input-group-container"]}>
          <div className={classes['filter-input-group']}>
            <input
              type="text"
              name="createdUserFirstName"
              placeholder="Created User First Name"
              className={classes["filter-input"]}
            />
            <input
              type="text"
              name="createdUserLastName"
              placeholder="Created User Last Name"
              className={classes["filter-input"]}
            />
            <input
              type="text"
              name="content"
              placeholder="Content"
              className={classes["filter-input"]}
            />
          </div>
          <div className='d-flex justify-content-end'>
          <button className={`me-2 ${classes["create-post-button"]}`} onClick={toggleModal}>Create</button>
            <button className={classes["search-button"]} type="submit">Search</button>
          </div>
        </div>
      </form>
      <div className='d-flex justify-content-center align-items-center flex-column'>
        {posts.map(post => (
          <PostComponent handlePostRefresh={handlePostRefresh} key={post.postId} post={post} />
        ))}
      </div>
      <ReactPaginate
        pageCount={totalPages}
        pageRangeDisplayed={3}
        marginPagesDisplayed={1}
        onPageChange={handlePageChange}
        containerClassName={paginationClasses.pagination}
        activeClassName={paginationClasses.active}
        breakClassName={paginationClasses.break}
        previousLabel={'<'}
        nextLabel={'>'}
        previousClassName={paginationClasses['pagination-arrow']}
        nextClassName={paginationClasses['pagination-arrow']}
        pageClassName={paginationClasses['pagination-button']}
        forcePage={pageNumber - 1}
      />
      {isModalOpen && (
        <div className={modalClasses['modal']}>
          <div className={modalClasses['modal-content']}>
            <h2 className={authClasses['auth-header']}>Create Post</h2>
            <form className={classes['post-form']} onSubmit={handleSubmit}>
              <div className="mb-5">
                <input
                  type="file"
                  accept="image/*"
                  lang="en"
                  className={authClasses['auth-input']}
                  onChange={(e) => setPhoto(e.target.files[0])}
                  required
                />
              </div>
              <div>
                <textarea
                  className={`${classes['post-content']} ${authClasses['auth-input']}`}
                  value={content}
                  onChange={(e) => setContent(e.target.value)}
                  required
                  placeholder="Content"
                  maxLength='1000'
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