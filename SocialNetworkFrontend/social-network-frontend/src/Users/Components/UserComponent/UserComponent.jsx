import React from 'react';
import classes from './UserComponent.module.scss';
import { baseUrl, authorization } from '../../../Shared/Options/ApiOptions';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faUserGroup } from '@fortawesome/free-solid-svg-icons';
import fonts from '../../../Shared/fonts.module.scss';
import icons from '../../../Shared/icons.module.scss';
import axios from 'axios';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const UserComponent = ({ user }) => {
    const [isFriend, setIsFriend] = useState(user.isFriend);
    const [isInvited, setIsInvited] = useState(user.isInvited);
    const navigate = useNavigate();

    const handleAddFriend = async () => {
        try {
            await axios.put(
                `${baseUrl}/user/${user.userId}/add-friend`,
                null,
                authorization(localStorage.getItem("token"))
            );
            setIsInvited(true);
        } catch (err) {
            alert(err.response.data);
        }
    }

    const handleDeleteFriend = async () => {
        try {
            await axios.put(
                `${baseUrl}/user/${user.userId}/delete-friend`,
                null,
                authorization(localStorage.getItem("token"))
            );
            setIsFriend(false);
        } catch (err) {
        }
    }

    const handleNavigateToUserDetails = () => {
        navigate(`/users/${user.userId}`);
    };

    return (
        <div key={user.userId} className={`${classes["user-item"]} d-flex align-items-center mb-5`}>
            <div>
                <img
                    src={`${baseUrl}/user/${user.userId}/profile-picture`}
                    alt="Profile"
                    className={classes['user-profile-picture']}
                    onClick={handleNavigateToUserDetails} />
            </div>
            <div className='ms-3'>
                <div className={fonts["font-green-medium"]}>
                    {user.firstName} {user.lastName}
                </div>
                <div className={fonts["font-grey-small"]}>
                    {user.city == null ? "No city" : user.city}, {user.country == null ? "No country" : user.country}
                </div>
                <div className='d-flex'>
                    <div className={fonts["font-green-small"]}>
                        <FontAwesomeIcon icon={faUser} className={icons.icon} /> {user.friendsCount}
                    </div>
                    {user.mutualFriendsCount !== undefined && (
                        <div className={`${fonts["font-green-small"]} ms-2`}>
                            <FontAwesomeIcon icon={faUserGroup} className={icons.icon} /> {user.mutualFriendsCount}
                        </div>
                    )}
                </div>
            </div>
            {isFriend && (
                <button onClick={handleDeleteFriend} className={`${classes["user-button"]} ${classes["user-button-delete"]}`}>Delete</button>
            )}
            {isInvited && (
                <button className={`${classes["user-button"]} ${classes["user-button-accept"]}`}>Invited</button>
            )}
            {!isInvited && !isFriend && (
                <button onClick={handleAddFriend} className={`${classes["user-button"]} ${classes["user-button-accept"]}`}>Add</button>
            )}
        </div>
    );
};

export default UserComponent;