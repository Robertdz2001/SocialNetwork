import React from 'react';
import classes from './UserComponent.module.scss';
import { baseUrl, authorization } from '../../../Shared/Options/ApiOptions';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import fonts from '../../../Shared/fonts.module.scss';
import icons from '../../../Shared/icons.module.scss';
import axios from 'axios';
import { useState } from 'react';

const UserComponent = ({ user }) => {
    const [isFriend, setIsFriend] = useState(user.isFriend);
    const [isInvited, setIsInvited] = useState(user.isInvited);

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
        } catch (err) {
        }
    }

    return (
        <div key={user.userId} className={`${classes["user-item"]} d-flex align-items-center mb-5`}>
            <div>
                <img src={`${baseUrl}/user/${user.userId}/profile-picture`} alt="Profile" className={classes['user-profile-picture']} />
            </div>
            <div className='ms-3'>
                <div className={fonts["font-green-medium"]}>
                    {user.firstName} {user.lastName}
                </div>
                <div className={fonts["font-grey-small"]}>
                    {user.city == null ? "No city" : user.city}, {user.country == null ? "No country" : user.country}
                </div>
                <div className={fonts["font-green-small"]}>
                    <FontAwesomeIcon icon={faUser} className={icons.icon} /> 32
                </div>
            </div>
            {isFriend && (
                <button onClick={handleDeleteFriend} className={classes["user-button"]}>Delete</button>
            )}
            {isInvited && (
                <button className={classes["user-button"]}>Invited</button>
            )}
            {!isInvited && !isFriend && (
                <button onClick={handleAddFriend} className={classes["user-button"]}>Add</button>
            )}
        </div>
    );
};

export default UserComponent;