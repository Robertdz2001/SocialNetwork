import React, { useState, useEffect } from 'react';
import axios from 'axios';
import ReactPaginate from 'react-paginate';
import { baseUrl } from '../Shared/Options/ApiOptions';
import classes from './MutualFriendsPage.module.scss';
import UserComponent from '../Users/Components/UserComponent/UserComponent.jsx';
import paginationClasses from '../Shared/pagination.module.scss';

const MutualFriendsPage = () => {
    const [mutualFriends, setMutualFriends] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    const fetchMutualFriends = async (currentPage) => {
        try {
            const token = localStorage.getItem('token');

            const params = {
                PageNumber: currentPage
            };

            const response = await axios.get(`${baseUrl}/user/mutual-friends`, {
                params,
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            setMutualFriends(response.data.items);
            setTotalPages(response.data.totalPages);
        } catch (error) {
            console.error(error.message);
        }
    };

    useEffect(() => {
        fetchMutualFriends(pageNumber);
    }, [pageNumber]);

    const handlePageChange = ({ selected }) => {
        const newPageNumber = selected + 1;
        setPageNumber(newPageNumber);
    };

    return (
        <div>
            <div className='d-flex justify-content-center align-items-center flex-column'>
                {mutualFriends.map(mutualFriend => (
                    <UserComponent key={mutualFriend.userId} user={mutualFriend} />
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
        </div>
    );
};

export default MutualFriendsPage;
