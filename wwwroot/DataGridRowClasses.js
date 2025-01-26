function setSlotClasses() {
    // Get the container with the ID 'MyGrid'
    const grid = document.getElementById("MyGrid");

    if (!grid) {
        console.warn("No element with ID 'MyGrid' found.");
        return;
    }

    // Find all <fluent-data-grid-cell> elements with grid-column="2" inside the grid or shadow DOM
    const shadowRoot = grid.shadowRoot || grid;
    const cells = shadowRoot.querySelectorAll('fluent-data-grid-cell[grid-column="2"]');

    // Loop through the filtered cells
    Array.from(cells).forEach(cell => {
        // Get the text content of the cell
        const cellText = cell.textContent.trim();

        // Set the class to the value of the text (or modify logic as needed)
        cell.className = cellText;
    });
}
