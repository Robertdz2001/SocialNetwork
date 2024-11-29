import React, { useState, useEffect } from "react";
import classes from './GroupsPage.module.scss';
import authClasses from '../Authentication/AuthenticationPage.module.scss';
import { baseUrl } from "../Shared/Options/ApiOptions";
import axios from "axios";
import ReactPaginate from 'react-paginate';
import paginationClasses from '../Shared/pagination.module.scss';
import GroupComponent from "./GroupComponent/GroupComponent.jsx";
import modalClasses from "../Shared/modals.module.scss";

const GroupsPage = () => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [photo, setPhoto] = useState(null);
    const [groups, setGroups] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [filters, setFilters] = useState({
        showOnlyWhereIsOwner: false,
        showOnlyWhereIsMember: false,
        name: ''
    });

    const fetchGroups = async (currentPage, searchFilters) => {
        try {
            const token = localStorage.getItem('token');

            const params = {
                ShowOnlyWhereIsOwner: searchFilters.showOnlyWhereIsOwner,
                ShowOnlyWhereIsMember: searchFilters.showOnlyWhereIsMember,
                Name: searchFilters.name,
                PageNumber: currentPage
            };

            const response = await axios.get(`${baseUrl}/group`, {
                params,
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            setGroups(response.data.items);
            setTotalPages(response.data.totalPages);
        } catch (error) {
            console.error(error.message);
        }
    };

    useEffect(() => {
        fetchGroups(pageNumber, filters);
    }, [pageNumber, filters]);

    const handleSearch = (e) => {
        e.preventDefault();
        setPageNumber(1);
    };

    const handleCheckboxChange = (e) => {
        const { name, checked } = e.target;
        setFilters(prevFilters => ({
            ...prevFilters,
            [name]: checked
        }));
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFilters(prevFilters => ({
            ...prevFilters,
            [name]: value
        }));
    };

    const handlePageChange = ({ selected }) => {
        setPageNumber(selected + 1);
    };

    const toggleModal = () => {
        setIsModalOpen(!isModalOpen);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const formData = new FormData();
        formData.append("Name", name);
        formData.append("Description", description);
        if (photo) formData.append("Photo", photo);

        try {
            const token = localStorage.getItem('token');
            await axios.post(`${baseUrl}/group`, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                    Authorization: `Bearer ${token}`,
                },
            });

            alert("Group created!");
            setName("");
            setDescription("");
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
                            name="name"
                            placeholder="Group Name"
                            className={classes["filter-input"]}
                            value={filters.name}
                            onChange={handleInputChange}
                        />
                        <label htmlFor="showOnlyWhereIsOwner">Show only owned groups</label>
                        <input
                            type="checkbox"
                            name="showOnlyWhereIsOwner"
                            checked={filters.showOnlyWhereIsOwner}
                            className={classes["filter-input"]}
                            onChange={handleCheckboxChange}
                        />
                        <label htmlFor="showOnlyWhereIsMember">Show only groups where you are a member</label>
                        <input
                            type="checkbox"
                            name="showOnlyWhereIsMember"
                            checked={filters.showOnlyWhereIsMember}
                            className={classes["filter-input"]}
                            onChange={handleCheckboxChange}
                        />
                    </div>
                    <div className='d-flex justify-content-end'>
                        <button className={`me-2 ${classes["create-group-button"]}`} onClick={toggleModal}>Create</button>
                        <button className={classes["search-button"]} type="submit">Search</button>
                    </div>
                </div>
            </form>
            <div className='d-flex justify-content-center align-items-center flex-column'>
                {groups.map(group => (
                    <GroupComponent key={group.id} group={group} />
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
                        <h2 className={authClasses['auth-header']}>Create Group</h2>
                        <form className={classes['group-form']} onSubmit={handleSubmit}>
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
                                    className={`${classes['group-name']} ${authClasses['auth-input']}`}
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                    required
                                    placeholder="Name"
                                    maxLength='30'
                                />
                            </div>
                            <div>
                                <textarea
                                    className={`${classes['group-description']} ${authClasses['auth-input']}`}
                                    value={description}
                                    onChange={(e) => setDescription(e.target.value)}
                                    required
                                    placeholder="Description"
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

export default GroupsPage;
