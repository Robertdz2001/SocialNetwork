import React from 'react';
import classes from './FriendInviteComponent.module.scss';
import { baseUrl, authorization } from '../../Shared/Options/ApiOptions';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import fonts from '../../Shared/fonts.module.scss';
import icons from '../../Shared/icons.module.scss';
import axios from 'axios';
import { useState } from 'react';

const FriendInviteComponent = ({ friendInvite }) => {
    const [isInviteAnswered, setIsInviteAnswered] = useState(false);

    const handleAcceptFriend = async () => {
        try {
            await axios.put(
                `${baseUrl}/user/answer-friend-invite`,
                {
                    userId: friendInvite.userId,
                    isAccepted: true
                },
                authorization(localStorage.getItem("token"))
            );
            setIsInviteAnswered(true);
        } catch (err) {
            alert(err.response.data);
        }
    }

    const handleRejectFriend = async () => {
        try {
            await axios.put(
                `${baseUrl}/user/answer-friend-invite`,
                {
                    userId: friendInvite.userId,
                    isAccepted: false
                },
                authorization(localStorage.getItem("token"))
            );
            setIsInviteAnswered(true);
        } catch (err) {
            alert(err.response.data);
        }
    }

    return (
        <>
            {!isInviteAnswered && (
                <div key={friendInvite.userId} className={`${classes["friend-invite-item"]} d-flex align-items-center mb-5`}>
                    <div>
                        <img src={`${baseUrl}/user/${friendInvite.userId}/profile-picture`} alt="Profile" className={classes['friend-invite-profile-picture']} />
                    </div>
                    <div className='ms-3'>
                        <div className={fonts["font-green-medium"]}>
                            {friendInvite.firstName} {friendInvite.lastName}
                        </div>
                        <div className={fonts["font-grey-small"]}>
                            {friendInvite.city == null ? "No city" : friendInvite.city}, {friendInvite.country == null ? "No country" : friendInvite.country}
                        </div>
                        <div className={fonts["font-green-small"]}>
                            <FontAwesomeIcon icon={faUser} className={icons.icon} /> {friendInvite.friendsCount}
                        </div>
                    </div>
                    <button onClick={handleRejectFriend} className={`${classes["friend-invite-button"]} ${classes["friend-invite-button-reject"]}`}>Reject</button>
                    <button onClick={handleAcceptFriend} className={`${classes["friend-invite-button"]} ${classes["friend-invite-button-accept"]}`}>Accept</button>
                </div>
            )}
        </>
    );
};

export default FriendInviteComponent;