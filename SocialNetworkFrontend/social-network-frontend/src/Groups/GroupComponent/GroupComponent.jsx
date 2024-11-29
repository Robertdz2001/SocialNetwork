import React from 'react';
import classes from './GroupComponent.module.scss';
import { baseUrl } from '../../Shared/Options/ApiOptions.js';
import fonts from '../../Shared/fonts.module.scss';

const GroupComponent = ({ group }) => {
    return (
        <div key={group.id} className={`${classes["group-item"]} mb-5`}>
            <div className='d-flex justify-content-center mt-2'>
                <img
                    src={`${baseUrl}/group/${group.id}/photo`}
                    alt="Group Photo"
                    className={classes['group-photo']} />
            </div>
            <div className='d-flex justify-content-center mt-2'>
                <div className={`${fonts["font-green-medium"]} ${classes['group-name']}`}>
                    {group.name}
                </div>
            </div>
        </div>
    );
};

export default GroupComponent;