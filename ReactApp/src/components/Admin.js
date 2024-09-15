import React, { useState, useEffect } from "react";
import { triggerCampaign } from "../api/httpService";

function Admin() {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        async function fetchUsers() {
            const { data } = await triggerCampaign.get("/api/users");
            setUsers(data);
        }
        fetchUsers();
    }, []);

    return (
        <div>
            <h1>Admin: Manage Users</h1>
            <ul>
                {users.map(user => (
                    <li key={user.id}>{user.email}</li>
                ))}
            </ul>
            {/* Implement add/edit/delete user actions */}
        </div>
    );
}

export default Admin;
