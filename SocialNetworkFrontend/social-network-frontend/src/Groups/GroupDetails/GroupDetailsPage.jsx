import { useParams } from "react-router-dom";
import { useEffect, useState, useRef } from "react";
import { useNavigate } from 'react-router-dom';
import axios from "axios";
import { baseUrl, authorization } from "../../Shared/Options/ApiOptions.js";
import classes from './GroupDetailsPage.module.scss';
import fonts from '../../Shared/fonts.module.scss';
import icons from '../../Shared/icons.module.scss';
import { faPager, faUser } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import MemberShortInfoComponent from "./Components/MemberShortInfoComponent/MemberShortInfoComponent.jsx";
import PostComponent from "../../Shared/Components/PostComponent/PostComponent.jsx";
import authClasses from '../../Authentication/AuthenticationPage.module.scss';
import modalClasses from '../../Shared/modals.module.scss';

function GroupDetailsPage() {
    const navigate = useNavigate();
    const { id } = useParams();
    const [canDelete, setDelete] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [content, setContent] = useState("");
    const [photo, setPhoto] = useState(null);
    const [canCreatePost, setCanCreatePost] = useState(null);
    const [chosenNav, setChosenNav] = useState('Members');
    const [groupData, setGroupData] = useState(null);
    const [groupPosts, setGroupPosts] = useState(null);
    const memberNavRef = useRef(null);
    const postNavRef = useRef(null);
    const [triggerEffect, setTriggerEffect] = useState(false);


    useEffect(() => {
        const fetchGroupData = async () => {
            try {
                const response = await axios.get(`${baseUrl}/group/${id}`, authorization(localStorage.getItem("token")));
                const responsePosts = await axios.get(`${baseUrl}/post/group/${id}`, authorization(localStorage.getItem("token")));

                setGroupData(response.data);
                setGroupPosts(responsePosts.data);
                setDelete(response.data.canDelete);
                setCanCreatePost(response.data.canCreatePost);
            } catch (error) {
                console.error("Error fetching group data:", error);
            }
        };
        fetchGroupData();
    }, [id, triggerEffect]);

    const handleDeleteGroup = async () => {
        if (!window.confirm("Are you sure you want to delete this group?")) {
            return;
        }

        try {
            await axios.delete(
                `${baseUrl}/group/${groupData.id}`,
                authorization(localStorage.getItem("token"))
            );
            navigate(`/groups`);
        } catch (error) {
            alert(error.response.data);
        }
    }

    const toggleModal = () => {
        setIsModalOpen(!isModalOpen);
      };

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        const formData = new FormData();
        formData.append("Content", content);
        formData.append("GroupId", groupData.id);
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
          setTriggerEffect(prev => !prev);
        } catch (error) {
          alert(error.response.data);
        }
      };

    const handleMemberNav = () => {
        memberNavRef.current.classList.add(classes['group-details-nav-button-active']);
        postNavRef.current.classList.remove(classes['group-details-nav-button-active']);
        setChosenNav('Members');
    }

    const handlePostNav = () => {
        memberNavRef.current.classList.remove(classes['group-details-nav-button-active']);
        postNavRef.current.classList.add(classes['group-details-nav-button-active']);
        setChosenNav('Posts');
    }

    const handlePostRefresh = () => {
        setTriggerEffect(prev => !prev);
    };

    return (
        <div>
            {groupData && groupPosts ? (
                <div className='d-flex align-items-center flex-column'>
                    <div className={`${classes['group-details-container']} d-flex`}>
                        <img
                            src={`${baseUrl}/group/${groupData.id}/photo`}
                            alt="Photo"
                            className={classes['group-details-photo']} />
                        <div>
                            <div className={`${fonts["font-green-large"]} d-flex align-items-center ms-4`}>
                                {groupData.name}
                            </div>
                            <div className={`${fonts["font-green-small"]} d-flex align-items-center ms-4`}>
                                {groupData.description}
                            </div>
                        </div>
                        {canCreatePost && (
                            <button onClick={toggleModal} className={`${classes["group-button"]} ${classes["group-button-accept"]}`}>Create Post</button>
                        )}
                        {canDelete && (
                            <button onClick={handleDeleteGroup} className={`${classes["group-button"]} ${classes["group-button-delete"]}`}>Delete</button>
                        )}
                    </div>
                    <div className="mt-3 d-flex">
                        <div>
                            <button ref={memberNavRef} onClick={handleMemberNav} className={`${classes['group-details-nav-button']} ${classes['group-details-nav-button-active']}`}><FontAwesomeIcon icon={faUser} className={icons.icon} /> {groupData.members.length}</button>
                        </div>
                        <div className="ms-4">
                            <button ref={postNavRef} onClick={handlePostNav} className={classes['group-details-nav-button']}><FontAwesomeIcon icon={faPager} className={icons.icon} /> {groupPosts.length}</button>
                        </div>
                    </div>
                    {chosenNav === 'Members' ? (
                        <div className={`${classes['members-container']} mt-4`}>
                            {groupData.members.map(member => (
                                <MemberShortInfoComponent key={member.id} member={member} groupId={groupData.id} handleRefresh={handlePostRefresh} />
                            ))}
                        </div>
                    ) : (
                        <div className={`${classes['members-container']} mt-4`}>
                            {groupPosts.map(post => (
                                <PostComponent handlePostRefresh={handlePostRefresh} key={post.postId} post={post} />
                            ))}
                        </div>
                    )}
                </div>
            ) : (
                <p>Loading group data...</p>
            )}
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

export default GroupDetailsPage;
