import { render, screen, fireEvent } from '@testing-library/react';
import GameModal from '../../components/GameModal';
import { vi } from 'vitest';

describe('GameModal', () => {
    const closeModal = vi.fn();
    const onGameAdded = vi.fn();

    it('renders the modal with form fields and buttons', () => {
        render(<GameModal closeModal={closeModal} onGameAdded={onGameAdded} />);

        // Check if all input fields are rendered
        expect(screen.getByLabelText(/Name:/i)).toBeInTheDocument();
        expect(screen.getByLabelText(/Min range:/i)).toBeInTheDocument();
        expect(screen.getByLabelText(/Max range:/i)).toBeInTheDocument();

        // Check for default rule input fields
        expect(screen.getByLabelText(/Divisor:/i)).toBeInTheDocument();
        expect(screen.getByLabelText(/Word:/i)).toBeInTheDocument();

        // Check for buttons
        expect(screen.getByText(/Add rule/i)).toBeInTheDocument();
        expect(screen.getByText(/Create Game/i)).toBeInTheDocument();
    });

    it('allows the user to input data in the form fields', () => {
        render(<GameModal closeModal={closeModal} onGameAdded={onGameAdded} />);

        const nameInput = screen.getByLabelText(/Name:/i);
        fireEvent.change(nameInput, { target: { value: 'Test Game' } });
        expect(nameInput).toHaveValue('Test Game');

        const minRangeInput = screen.getByLabelText(/Min range:/i);
        fireEvent.change(minRangeInput, { target: { value: '10' } });
        expect(minRangeInput).toHaveValue(10);

        const maxRangeInput = screen.getByLabelText(/Max range:/i);
        fireEvent.change(maxRangeInput, { target: { value: '20' } });
        expect(maxRangeInput).toHaveValue(20);

        const divisorInput = screen.getByLabelText(/Divisor:/i);
        fireEvent.change(divisorInput, { target: { value: '3' } });
        expect(divisorInput).toHaveValue(3);

        const wordInput = screen.getByLabelText(/Word:/i);
        fireEvent.change(wordInput, { target: { value: 'Fizz' } });
        expect(wordInput).toHaveValue('Fizz');
    });

    it('adds a new rule when "Add rule" button is clicked', () => {
        render(<GameModal closeModal={closeModal} onGameAdded={onGameAdded} />);

        const addRuleButton = screen.getByText(/Add rule/i);
        fireEvent.click(addRuleButton);

        const allDivisorInputs = screen.getAllByLabelText(/Divisor:/i);
        const allWordInputs = screen.getAllByLabelText(/Word:/i);

        // Check if new rule inputs are added
        expect(allDivisorInputs).toHaveLength(2);
        expect(allWordInputs).toHaveLength(2);
    });

    it('calls the closeModal function when clicking outside the modal', () => {
        const { container } = render(<GameModal closeModal={closeModal} onGameAdded={onGameAdded} />);

        const modalOverlay = container.querySelector('.modal-overlay');
        fireEvent.mouseDown(modalOverlay as Element);

        expect(closeModal).toHaveBeenCalled();
    });
});
