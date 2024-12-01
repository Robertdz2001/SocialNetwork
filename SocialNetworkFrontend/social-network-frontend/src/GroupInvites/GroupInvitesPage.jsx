import React, { useState, useEffect } from 'react';
import axios from 'axios';
import ReactPaginate from 'react-paginate';
import { baseUrl } from '../Shared/Options/ApiOptions';
import paginationClasses from '../Shared/pagination.module.scss';
import GroupInviteComponent from './Components/GroupInviteComponent';

const GroupInvitesPage = () => {
  const [groupInvites, setGroupInvites] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [totalPages, setTotalPages] = useState(1);

  const fetchGroupInvites = async (currentPage) => {
    try {
      const token = localStorage.getItem('token');

      const params = {
        PageNumber: currentPage
      };

      const response = await axios.get(`${baseUrl}/group/group-invites`, {
        params,
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      setGroupInvites(response.data.items);
      setTotalPages(response.data.totalPages);
    } catch (error) {
      console.error(error.message);
    }
  };

  useEffect(() => {
    fetchGroupInvites(pageNumber);
  }, [pageNumber]);

  const handlePageChange = ({ selected }) => {
    const newPageNumber = selected + 1;
    setPageNumber(newPageNumber);
  };

  return (
    <div>
      <div className='d-flex justify-content-center'>
        <ul>
        {groupInvites.map(groupInvite => (
          <GroupInviteComponent key={groupInvite.id} groupInvite={groupInvite} />
        ))}
        </ul>
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

export default GroupInvitesPage;
