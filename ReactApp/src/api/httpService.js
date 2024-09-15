﻿// src/api/httpService.js
const API_BASE_URL = 'https://localhost:7287/api'; // Replace with your actual API base URL

const fetchData = async (endpoint, options = {}) => {
    const response = await fetch(`${API_BASE_URL}/${endpoint}`, {
        ...options,
        headers: {
            'Content-Type': 'application/json',
            ...options.headers,
        },
    });

    if (!response.ok) {
        throw new Error('Network response was not ok');
    }

    return response.json();
};

export const login = async (credentials) => {
    return fetchData('account/login', {
        method: 'POST',
        body: JSON.stringify(credentials),
    });
};

export const triggerCampaign = async (campaignData, token) => {
    const response = await fetch(`${API_BASE_URL}/campaign/TriggerCampaign`, {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(campaignData),
    });

    if (!response.ok) {
        throw new Error('Failed to trigger campaign');
    }

    return response.json();
};
