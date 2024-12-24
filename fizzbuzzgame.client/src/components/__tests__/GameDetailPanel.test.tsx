// src/__tests__/components/GameDetailPanel.test.tsx

import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { vi } from 'vitest';  // Import vitest for mocks
import GameDetailPanel, { Rule } from '../../components/GameDetailPanel';
import { MemoryRouter } from 'react-router-dom';

// Mocking necessary functions using vi.importActual to import the actual module
vi.mock('react-router-dom', async () => {
    const actual = await vi.importActual('react-router-dom');
    return {
        ...actual,
        useNavigate: vi.fn(), // Mocking useNavigate here
    };
});

describe('GameDetailPanel', () => {
    const mockCloseModal = vi.fn();  // Mock closeModal function
    const mockNavigate = vi.fn();  // Mock navigate function

    const rules: Rule[] = [
        { divisor: 3, word: 'Fizz' },
        { divisor: 5, word: 'Buzz' },
    ];

    beforeEach(() => {
        // Clear mocks before each test
        vi.clearAllMocks();
    });

    it('renders game details correctly', () => {
        render(
            <MemoryRouter>
                <GameDetailPanel
                    id={1}
                    name="FizzBuzz Game"
                    minRange={1}
                    maxRange={100}
                    rules={rules}
                    closeModal={mockCloseModal}
                />
            </MemoryRouter>
        );

        // Check that the name, range, and rules are rendered
        expect(screen.getByText('FizzBuzz Game')).toBeInTheDocument();
        expect(screen.getByText('Number range: 1 to 100')).toBeInTheDocument();
        expect(screen.getByText('3 - Fizz')).toBeInTheDocument();
        expect(screen.getByText('5 - Buzz')).toBeInTheDocument();
    });

    it('handles input change correctly', () => {
        render(
            <MemoryRouter>
                <GameDetailPanel
                    id={1}
                    name="FizzBuzz Game"
                    minRange={1}
                    maxRange={100}
                    rules={rules}
                    closeModal={mockCloseModal}
                />
            </MemoryRouter>
        );

        const input = screen.getByPlaceholderText('Enter game duration (minimum 60s)') as HTMLInputElement;

        // Simulate user typing in the input field
        fireEvent.change(input, { target: { value: '120' } });

        // Check that the input value is updated
        expect(input.value).toBe('120');
    });
});
