import { useParams } from "react-router-dom";
import { useEffect, useState, useRef } from "react";
import axios from "axios";
import { baseUrl, authorization } from "../../Shared/Options/ApiOptions.js";
import classes from './UserDetailsPage.module.scss';
import fonts from '../../Shared/fonts.module.scss';
import icons from '../../Shared/icons.module.scss';
import authClasses from '../../Authentication/AuthenticationPage.module.scss';
import modalClasses from '../../Shared/modals.module.scss';
import { faPager, faPhone, faUser, faUserGroup } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import UserShortInfoComponent from "./Components/UserShortInfoComponent/UserShortInfoComponent.jsx";
import PostComponent from "../../Shared/Components/PostComponent/PostComponent.jsx";

function UserDetailsPage() {
    const { id } = useParams();
    const [isFriend, setIsFriend] = useState(null);
    const [isInvited, setIsInvited] = useState(null);
    const [isBlocked, setIsBlocked] = useState(null);
    const [canBlock, setCanBlock] = useState(null);
    const [chosenNav, setChosenNav] = useState('Users');
    const [userData, setUserData] = useState(null);
    const [userPosts, setUserPosts] = useState(null);
    const userNavRef = useRef(null);
    const postNavRef = useRef(null);
    const [groupsForInvite, setGroupsForInvite] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [triggerEffect, setTriggerEffect] = useState(false);


    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const response = await axios.get(`${baseUrl}/user/${id}`, authorization(localStorage.getItem("token")));
                const responsePosts = await axios.get(`${baseUrl}/post/user/${id}`, authorization(localStorage.getItem("token")));

                setUserData(response.data);
                setUserPosts(responsePosts.data);
                setIsFriend(response.data.isFriend);
                setIsInvited(response.data.isInvited);
                setIsBlocked(response.data.isBlocked);
                setCanBlock(response.data.canBlockUser);
            } catch (error) {
                console.error("Error fetching user data:", error);
            }
        };
        fetchUserData();
    }, [id, triggerEffect]);

    const fetchGroupsForInvite = async () => {
        try {
            const response = await axios.get(`${baseUrl}/group/groups-for-invite/${id}`, authorization(localStorage.getItem("token")));
            setGroupsForInvite(response.data);
            setIsModalOpen(true);
        } catch (error) {
            console.error("Error fetching groups for invite:", error);
        }
    };

    const handleInviteToGroup = async (groupId) => {
        try {
            await axios.put(
                `${baseUrl}/group/${groupId}/invite/${id}`,
                null,
                authorization(localStorage.getItem("token"))
            );
            alert("User invited to the group successfully!");
            setGroupsForInvite(prevGroups => prevGroups.filter(group => group.id !== groupId));
        } catch (err) {
            alert("Failed to invite user to the group.");
        }
    };

    const handleAddFriend = async () => {
        try {
            await axios.put(
                `${baseUrl}/user/${userData.id}/add-friend`,
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
                `${baseUrl}/user/${userData.id}/delete-friend`,
                null,
                authorization(localStorage.getItem("token"))
            );
            setIsFriend(false);
        } catch (err) {
        }
    }

    const handleToggleBlock = async () => {
        const confirmationMessage = isBlocked
            ? "Are you sure you want to unlock this user?"
            : "Are you sure you want to block this user?";
        if (!window.confirm(confirmationMessage)) {
            return;
        }

        try {
            await axios.put(
                `${baseUrl}/user/${userData.id}/toggle-block`,
                null,
                authorization(localStorage.getItem("token"))
            );
            setIsBlocked(!isBlocked);
        } catch (err) {
        }
    }

    const handleUserNav = () => {
        userNavRef.current.classList.add(classes['user-details-nav-button-active']);
        postNavRef.current.classList.remove(classes['user-details-nav-button-active']);
        setChosenNav('Users');
    }

    const handlePostNav = () => {
        userNavRef.current.classList.remove(classes['user-details-nav-button-active']);
        postNavRef.current.classList.add(classes['user-details-nav-button-active']);
        setChosenNav('Posts');
    }

    const handlePostRefresh = () => {
        setTriggerEffect(prev => !prev);
    };

    return (
        <div>
            {userData && userPosts ? (
                <div className='d-flex align-items-center flex-column'>
                    <div className={`${classes['user-details-container']} d-flex`}>
                        <img
                            src={`${baseUrl}/user/${userData.id}/profile-picture`}
                            alt="Profile"
                            className={classes['user-details-profile-picture']} />
                        <div className={`${fonts["font-green-large"]} d-flex align-items-center ms-4`}>
                            {userData.firstName} {userData.lastName}
                        </div>
                        <div className={`${fonts["font-grey-medium"]} d-flex justify-content-center flex-column ms-4`}>
                            <p className={`${classes['user-details-margin']} text-truncate`}>{userData.country}</p>
                            <p className={`${classes['user-details-margin']} text-truncate`}>{userData.city}</p>
                            <div className='d-flex align-items-center'>
                                <FontAwesomeIcon icon={faPhone} className={icons.icon} />
                                <p className="ms-2 m-0">{userData.phoneNumber}</p>
                            </div>
                        </div>
                        {!userData.isItMyUser && (
                            <>
                                {isFriend && (
                                    <button onClick={handleDeleteFriend} className={`${classes["user-button"]} ${classes["user-button-delete"]}`}>Delete</button>
                                )}
                                {isInvited && (
                                    <button className={`${classes["user-button"]} ${classes["user-button-accept"]}`}>Invited</button>
                                )}
                                {!isInvited && !isFriend && (
                                    <button onClick={handleAddFriend} className={`${classes["user-button"]} ${classes["user-button-accept"]}`}>Add</button>
                                )}
                                {canBlock && (
                                    <button onClick={handleToggleBlock} className={`${classes["user-button"]} ${classes["user-button-block"]} ${isBlocked ? classes["user-button-accept"] : classes["user-button-delete"]}`}>{isBlocked ? "Unlock" : "Block"}</button>
                                )}
                                <button onClick={fetchGroupsForInvite} className={classes["invite-user-button"]}>Invite to group</button>
                            </>
                        )}
                    </div>
                    <div className="mt-3 d-flex">
                        <div>
                            <button ref={userNavRef} onClick={handleUserNav} className={`${classes['user-details-nav-button']} ${classes['user-details-nav-button-active']}`}><FontAwesomeIcon icon={faUser} className={icons.icon} /> {userData.friends.length}</button>
                        </div>
                        <div className="ms-4">
                            <button ref={postNavRef} onClick={handlePostNav} className={classes['user-details-nav-button']}><FontAwesomeIcon icon={faPager} className={icons.icon} /> {userPosts.length}</button>
                        </div>
                    </div>
                    {chosenNav === 'Users' ? (
                        <div className={`${classes['friends-container']} mt-4`}>
                            {userData.friends.map(friend => (
                                <UserShortInfoComponent key={friend.id} user={friend} />
                            ))}
                        </div>
                    ) : (
                        <div className={`${classes['friends-container']} mt-4`}>
                            {userPosts.map(post => (
                                <PostComponent handlePostRefresh={handlePostRefresh} key={post.postId} post={post} />
                            ))}
                        </div>
                    )}
                </div>
            ) : (
                <p>Loading user data...</p>
            )}
            {isModalOpen && (
                <div className={modalClasses['modal']}>
                    <div className={modalClasses['modal-content']}>
                        <div className="d-flex align-items-center mb-4 justify-content-between">
                            <h2 className={`${authClasses['auth-header']} m-0`}>Invite to group</h2>
                            <button className={classes.closeModal} onClick={() => setIsModalOpen(false)}>X</button>
                        </div>
                        <ul className={classes['group-container']}>
                            {groupsForInvite.map(group => (
                                <li key={group.id} className="d-flex justify-content-between align-items-center mb-3">
                                    <img
                                        src={`${baseUrl}/group/${group.id}/photo`}
                                        alt="Profile"
                                        className={classes['user-details-group-photo']} />
                                    <span className={fonts['font-green-medium']}>{group.name}</span>
                                    <button
                                        className={classes["invite-button"]}
                                        onClick={() => handleInviteToGroup(group.id)}>
                                        Invite
                                    </button>
                                </li>
                            ))}
                        </ul>
                    </div>
                </div>
            )}
        </div>
    );
}

export default UserDetailsPage;
