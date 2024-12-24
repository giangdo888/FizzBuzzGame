import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import PlayField from '../../components/PlayField';
import { vi } from 'vitest';
import { fetchData } from '../../services/api';

// Mock the fetchData function
vi.mock('../../services/api', () => ({
    fetchData: vi.fn(),
}));

describe('PlayField', () => {
    const mockFetchData = vi.mocked(fetchData);

    beforeEach(() => {
        mockFetchData.mockResolvedValue({
            question: { id: 1, question: 10 },
            isCorrect: true,
        });
    });

    afterEach(() => {
        vi.clearAllMocks();
    });

    it('renders the question, timer, and answer form', () => {
        render(<PlayField id={1} question={5} timeLimit={60} />);

        // Check if the question is rendered
        expect(screen.getByText(/5/i)).toBeInTheDocument();

        // Check if the timer is rendered
        expect(screen.getByText(/This question time left: 60/i)).toBeInTheDocument();

        // Check if the input field and button are rendered
        expect(screen.getByPlaceholderText(/Enter your answer here/i)).toBeInTheDocument();
        expect(screen.getByRole('button', { name: /Submit/i })).toBeInTheDocument();
    });

    it('allows the user to type an answer in the input field', () => {
        render(<PlayField id={1} question={5} timeLimit={60} />);

        const input = screen.getByPlaceholderText(/Enter your answer here/i);

        fireEvent.change(input, { target: { value: 'Fizz' } });
        expect(input).toHaveValue('Fizz');
    });

    it('submits the answer and updates the correct count for a correct answer', async () => {
        mockFetchData.mockResolvedValueOnce({
            question: { id: 1, question: 15 },
            isCorrect: true,
        });

        render(<PlayField id={1} question={5} timeLimit={60} />);
        const input = screen.getByPlaceholderText(/Enter your answer here/i);
        const submitButton = screen.getByRole('button', { name: /Submit/i });

        fireEvent.change(input, { target: { value: 'Fizz' } });
        fireEvent.click(submitButton);

        await waitFor(() => {
            expect(mockFetchData).toHaveBeenCalledWith('attempts/1/answer', {
                method: 'POST',
                body: { answer: 'Fizz' },
            });
        });

        expect(screen.getByText(/Correct answer: 1/i)).toBeInTheDocument();
        expect(screen.getByText(/15/i)).toBeInTheDocument(); // Next question
    });

    it('submits the answer and updates the incorrect count for an incorrect answer', async () => {
        mockFetchData.mockResolvedValueOnce({
            question: { id: 1, question: 20 },
            isCorrect: false,
        });

        render(<PlayField id={1} question={5} timeLimit={60} />);
        const input = screen.getByPlaceholderText(/Enter your answer here/i);
        const submitButton = screen.getByRole('button', { name: /Submit/i });

        fireEvent.change(input, { target: { value: 'Buzz' } });
        fireEvent.click(submitButton);

        await waitFor(() => {
            expect(mockFetchData).toHaveBeenCalledWith('attempts/1/answer', {
                method: 'POST',
                body: { answer: 'Buzz' },
            });
        });

        expect(screen.getByText(/Incorrect answer: 1/i)).toBeInTheDocument();
        expect(screen.getByText(/20/i)).toBeInTheDocument(); // Next question
    });

    it('resets the timer when a new question is fetched', async () => {
        mockFetchData.mockResolvedValueOnce({
            question: { id: 1, question: 30 },
            isCorrect: true,
        });

        render(<PlayField id={1} question={5} timeLimit={60} />);
        const submitButton = screen.getByRole('button', { name: /Submit/i });

        fireEvent.click(submitButton);

        await waitFor(() => {
            expect(screen.getByText(/This question time left: 60/i)).toBeInTheDocument();
        });
    });
});
