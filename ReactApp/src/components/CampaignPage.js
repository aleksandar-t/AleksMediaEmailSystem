// src/components/CampaignPage.js
import React, { useState } from 'react';
import { Form, Button, Container, Alert } from 'react-bootstrap';
import { triggerCampaign } from '../api/httpService';

const CampaignPage = () => {
    const [filePath, setFilePath] = useState('');
    const [error, setError] = useState(null);

    const handleFilePathChange = (e) => {
        setFilePath(e.target.value);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const token = localStorage.getItem('token');
        if (!token) {
            setError('No token found');
            return;
        }

        if (!filePath) {
            setError('No file path provided');
            return;
        }

        try {
            await triggerCampaign({ xmlFilePath: filePath }, token);
            alert('Campaign triggered successfully!');
            setError(null); // Clear error if successful
        } catch (err) {
            setError('Failed to trigger campaign');
        }
    };

    return (
        <Container className="mt-4" style={{ maxWidth: '600px' }}>
            <h2 className="mb-4">Trigger Campaign</h2>
            {error && <Alert variant="danger">{error}</Alert>}
            <Form onSubmit={handleSubmit}>
                <Form.Group controlId="formFilePath">
                    <Form.Label>File Path</Form.Label>
                    <Form.Control
                        type="text"
                        onChange={(e) => setFilePath(e.target.value)}
                        required
                    />
                </Form.Group>
                <Button variant="primary" type="submit" className="mt-3">
                    Trigger Campaign
                </Button>
            </Form>
        </Container>
    );
};

export default CampaignPage;
