import { render, screen, waitFor } from '@testing-library/react';
import Result from '../../components/Result';
import { vi } from 'vitest';
import { fetchData } from '../../services/api';

// Mock the fetchData function
vi.mock('../../services/api', () => ({
    fetchData: vi.fn(),
}));

describe('Result', () => {
    const mockFetchData = vi.mocked(fetchData);

    afterEach(() => {
        vi.clearAllMocks();
    });

    it('fetches and displays the result data', async () => {
        // Mock API response
        mockFetchData.mockResolvedValueOnce({
            correctNumber: 5,
            incorrectNumber: 3,
        });

        render(<Result id={1} />);

        // Check that the fetchData is called with the correct arguments
        await waitFor(() => {
            expect(mockFetchData).toHaveBeenCalledWith('attempts/1/finalize', {
                method: 'PUT',
                body: {},
            });
        });

        // Verify that the correct result is displayed
        expect(await screen.findByText(/Correct answer: 5/i)).toBeInTheDocument();
        expect(await screen.findByText(/Incorrect answer: 3/i)).toBeInTheDocument();
    });

    it('renders nothing if fetchData fails', async () => {
        // Mock API failure
        mockFetchData.mockRejectedValueOnce(new Error('API error'));

        render(<Result id={1} />);

        // Verify fetchData was called
        await waitFor(() => {
            expect(mockFetchData).toHaveBeenCalledWith('attempts/1/finalize', {
                method: 'PUT',
                body: {},
            });
        });

        // Check that no result data is displayed
        expect(screen.queryByText(/Correct answer:/i)).not.toBeInTheDocument();
        expect(screen.queryByText(/Incorrect answer:/i)).not.toBeInTheDocument();
    });
});
