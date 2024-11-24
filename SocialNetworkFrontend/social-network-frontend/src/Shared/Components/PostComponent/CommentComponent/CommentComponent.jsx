import React from 'react';
import classes from './CommentComponent.module.scss';
import { baseUrl, authorization, formatDate } from '../../../Options/ApiOptions.js';
import fonts from '../../../fonts.module.scss';
import icons from '../../../icons.module.scss';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCalendarDays } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const CommentComponent = ({ comment, postId, handleRefresh }) => {
    const navigate = useNavigate();

    const handleNavigateToUserDetails = () => {
        navigate(`/users/${comment.userId}`);
    };

    const handleDelete = async () => {
        if (!window.confirm("Are you sure you want to delete this comment?")) {
            return;
        }

        try {
            await axios.delete(
                `${baseUrl}/post/${postId}/comments/${comment.commentId}`,
                authorization(localStorage.getItem("token"))
            );
            handleRefresh();
        } catch (error) {
            alert(error.response.data);
        }
    }

    return (
        <div key={comment.commentId} className={`${classes['comment-item']} mb-5`}>
            {comment.canDelete && (
                <button onClick={handleDelete} className={`${classes["comment-button"]} ${classes["comment-button-delete"]}`}>Delete</button>
            )}
            <div className='d-flex align-items-center justify-content-between mb-2 me-3'>
                <div className='d-flex align-items-center'>
                    <div className='ms-2'>
                        <img
                            src={`${baseUrl}/user/${comment.userId}/profile-picture`}
                            alt="Profile"
                            className={classes['comment-user-profile-picture']}
                            onClick={handleNavigateToUserDetails} />
                    </div>
                    <div className='ms-2'>
                        <div className={fonts["font-green-small"]}>
                            {comment.userFirstName} {comment.userLastName}
                        </div>
                    </div>
                </div>
                <div className={fonts["font-green-small"]}>
                    <FontAwesomeIcon icon={faCalendarDays} className={icons.icon} /> {formatDate(comment.created)}
                </div>
            </div>
            <div className={`${fonts["font-green-small"]} text-break`}>
                {comment.content}
            </div>
        </div>
    );
};

export default CommentComponent;