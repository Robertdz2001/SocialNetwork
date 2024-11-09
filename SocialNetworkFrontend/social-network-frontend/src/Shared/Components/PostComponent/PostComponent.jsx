import React from 'react';
import classes from './PostComponent.module.scss';
import { baseUrl, authorization } from '../../Options/ApiOptions.js';
import fonts from '../../fonts.module.scss';
import axios from 'axios';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const PostComponent = ({ post }) => {
    const navigate = useNavigate();

    const handleNavigateToUserDetails = () => {
        navigate(`/users/${post.createdUserId}`);
    };

    return (
        <div key={post.postId} className={`${classes["post-item"]} mb-5`}>
            <div className='d-flex align-items-center'>
                <div className='ms-2'>
                    <img
                        src={`${baseUrl}/user/${post.createdUserId}/profile-picture`}
                        alt="Profile"
                        className={classes['created-user-profile-picture']}
                        onClick={handleNavigateToUserDetails} />
                </div>
                <div className='ms-2'>
                    <div className={fonts["font-green-small"]}>
                        {post.createdUserFirstName} {post.createdUserLastName}
                    </div>
                </div>
            </div>
            <div className='d-flex justify-content-center mt-2'>
                <img
                    src={`${baseUrl}/post/${post.postId}/photo`}
                    alt="Post Photo"
                    className={classes['post-photo']} />
            </div>
            <div className='d-flex justify-content-center mt-2'>
                <div className={`${fonts["font-green-small"]} ${classes['post-content']}`}>
                    {post.content}
                </div>
            </div>
        </div>
    );
};

export default PostComponent;