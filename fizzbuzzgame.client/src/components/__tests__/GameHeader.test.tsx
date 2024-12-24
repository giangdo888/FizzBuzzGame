// src/components/__tests__/GameHeader.test.tsx
import { render, screen } from '@testing-library/react';
import GameHeader from '../../components/GameHeader';

describe('GameHeader', () => {
    it('renders the main header and instruction correctly', () => {
        const mainHeader = 'Welcome to the Game!';
        const instruction = 'Please follow the instructions to play';

        render(<GameHeader mainHeader={mainHeader} instruction={instruction} />);

        // Check that the main header is rendered
        expect(screen.getByText(mainHeader)).toBeInTheDocument();
        // Check that the instruction is rendered
        expect(screen.getByText(instruction)).toBeInTheDocument();
    });

    it('has the correct class name for the header element', () => {
        const mainHeader = 'Welcome to the Game!';
        const instruction = 'Please follow the instructions to play';

        render(<GameHeader mainHeader={mainHeader} instruction={instruction} />);

        const headerElement = screen.getByRole('banner');  // <header> is a landmark role
        expect(headerElement).toHaveClass('main-header');
    });
});
