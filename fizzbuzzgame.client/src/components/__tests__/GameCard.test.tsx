import { render, fireEvent } from "@testing-library/react";
import "@testing-library/jest-dom";
import GameCard from "../../components/GameCard";

describe("GameCard Component", () => {
    test("renders the name correctly", () => {
        // Arrange
        const name = "Test Game";
        const handleClick = vi.fn(); // using Vitest's `vi.fn()`

        // Act
        const { getByText } = render(<GameCard name={name} onClick={handleClick} />);

        // Assert
        expect(getByText(name)).toBeInTheDocument();
    });

    test("calls onClick when button is clicked", () => {
        // Arrange
        const name = "Test Game";
        const handleClick = vi.fn();

        // Act
        const { getByText } = render(<GameCard name={name} onClick={handleClick} />);
        const button = getByText(name);
        fireEvent.click(button);

        // Assert
        expect(handleClick).toHaveBeenCalledTimes(1);
    });

    test("applies the correct CSS class to the button", () => {
        // Arrange
        const name = "Test Game";
        const handleClick = vi.fn();

        // Act
        const { getByText } = render(<GameCard name={name} onClick={handleClick} />);
        const button = getByText(name);

        // Assert
        expect(button).toHaveClass("game-card-button");
    });
});
