import React from 'react';
import classes from './PostComponent.module.scss';
import { baseUrl, authorization, formatDate } from '../../Options/ApiOptions.js';
import fonts from '../../fonts.module.scss';
import icons from '../../icons.module.scss';
import axios from 'axios';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { faCalendarDays, faComment, faThumbsUp } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const PostComponent = ({ handlePostRefresh, post }) => {
    const navigate = useNavigate();

    const handleNavigateToUserDetails = () => {
        navigate(`/users/${post.createdUserId}`);
    };

    const handleLikeClick = async () => {
        try {
            await axios.put(
                `${baseUrl}/post/${post.postId}/toggle-like`, null,
                authorization(localStorage.getItem("token"))
            );
            handlePostRefresh();
        } catch (err) {
            alert(err.response.data);
        }
    };

    return (
        <div key={post.postId} className={`${classes["post-item"]} mb-5`}>
            <div className='d-flex align-items-center justify-content-between'>
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
                <div className={fonts["font-green-small"]}>
                    <FontAwesomeIcon icon={faCalendarDays} className={icons.icon} /> {formatDate(post.createdDate)}
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
            <div className={`${classes["post-stats"]} d-flex mt-2 ms-2 justify-content-between align-items-center`}>
                <div className={fonts["font-green-small"]}>
                    <FontAwesomeIcon icon={faThumbsUp} className={icons.icon} /> {post.userLikesCount}
                </div>
                <div className={fonts["font-green-small"]}>
                    <FontAwesomeIcon icon={faComment} className={icons.icon} /> 11
                </div>
            </div>
            <div className={`${classes["post-actions"]} d-flex mt-2 ms-2 justify-content-between align-items-center`}>
                <div className={fonts["font-green-small"]}>
                    <button onClick={handleLikeClick} className={`${classes['post-action']} ${post.isLiked && classes['post-action-active']}`}><FontAwesomeIcon icon={faThumbsUp} className={icons.icon} /> Like </button>
                </div>
                <div className={fonts["font-green-small"]}>
                    <button className={classes['post-action']}><FontAwesomeIcon icon={faComment} className={icons.icon} /> Show comments </button>
                </div>
            </div>
        </div>
    );
};

export default PostComponent;