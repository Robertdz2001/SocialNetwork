import React, { useState, useEffect } from 'react';
import axios from 'axios';
import ReactPaginate from 'react-paginate';
import { baseUrl } from '../Shared/Options/ApiOptions';
import classes from './UsersPage.module.scss';
import UserComponent from './Components/UserComponent/UserComponent';
import paginationClasses from '../Shared/pagination.module.scss';

const UsersPage = () => {
  const [users, setUsers] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [filters, setFilters] = useState({
    firstName: '',
    lastName: '',
    country: '',
    city: ''
  });

  const fetchUsers = async (currentPage, searchFilters) => {
    try {
      const token = localStorage.getItem('token');

      const params = {
        FirstName: searchFilters.firstName,
        LastName: searchFilters.lastName,
        Country: searchFilters.country,
        City: searchFilters.city,
        PageNumber: currentPage,
      };

      const response = await axios.get(`${baseUrl}/user`, {
        params,
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      setUsers(response.data.items);
      setTotalPages(response.data.totalPages);
    } catch (error) {
      console.error(error.message);
    }
  };

  useEffect(() => {
    fetchUsers(pageNumber, filters);
  }, [pageNumber, filters]);

  const handleSearch = (e) => {
    e.preventDefault();

    const newFilters = {
      firstName: e.target.firstName.value,
      lastName: e.target.lastName.value,
      country: e.target.country.value,
      city: e.target.city.value
    };

    // Ustaw nowe filtry i resetuj numer strony
    setFilters(newFilters);
    setPageNumber(1);
  };

  const handlePageChange = ({ selected }) => {
    const newPageNumber = selected + 1;
    setPageNumber(newPageNumber);
  };

  return (
    <div>
      <form onSubmit={handleSearch} className='d-flex justify-content-center'>
        <div className={classes["filter-input-group-container"]}>
          <div className={classes['filter-input-group']}>
            <input
              type="text"
              name="firstName"
              placeholder="First Name"
              className={classes["filter-input"]}
            />
            <input
              type="text"
              name="lastName"
              placeholder="Last Name"
              className={classes["filter-input"]}
            />
            <input
              type="text"
              name="country"
              placeholder="Country"
              className={classes["filter-input"]}
            />
            <input
              type="text"
              name="city"
              placeholder="City"
              className={classes["filter-input"]}
            />
          </div>
          <div className='d-flex justify-content-end'>
            <button className={classes["search-button"]} type="submit">Search</button>
          </div>
        </div>
      </form>
      <div className='d-flex justify-content-center align-items-center flex-column'>
        {users.map(user => (
          <UserComponent key={user.userId} user={user} />
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

export default UsersPage;
