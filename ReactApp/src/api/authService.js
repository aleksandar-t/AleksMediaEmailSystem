// src/api/httpService.js
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
    return fetchData('components/forms/login', {
        method: 'POST',
        body: JSON.stringify(credentials),
    });
};

export const triggerCampaign = async (filePath, token) => {
    return fetchData('components/forms/TriggerCampaign', {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${token}`,
        },
        body: JSON.stringify({ XmlFilePath: filePath }),
    });
};