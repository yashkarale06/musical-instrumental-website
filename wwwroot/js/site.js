// site.js - Basic functionality for Musical Instrument Shop

// Wait for the DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function() {
    // Handle add to cart buttons
    const addToCartButtons = document.querySelectorAll('.add-to-cart');
    if (addToCartButtons) {
        addToCartButtons.forEach(button => {
            button.addEventListener('click', function() {
                const productId = this.getAttribute('data-id');
                const quantity = document.getElementById('quantity') 
                    ? document.getElementById('quantity').value 
                    : 1;
                
                // In a real application, this would call an API endpoint
                // For now, just show an alert
                alert(`Added ${quantity} item(s) of product ${productId} to cart!`);
                
                // Update cart count in header (this is just for demo purposes)
                const cartLink = document.querySelector('.user-actions a:last-child');
                if (cartLink) {
                    const currentCount = parseInt(cartLink.textContent.match(/\d+/)[0] || '0');
                    const newCount = currentCount + parseInt(quantity);
                    cartLink.textContent = `Cart (${newCount})`;
                }
            });
        });
    }
    
    // Handle product filters
    const filterSelects = document.querySelectorAll('.products-filter select');
    if (filterSelects) {
        filterSelects.forEach(select => {
            select.addEventListener('change', function() {
                // In a real application, this would filter the products
                // For now, just show an alert
                console.log('Filter changed:', this.id, 'to', this.value);
            });
        });
    }
    
    // Handle tab switching on product details page
    const tabs = document.querySelectorAll('.tabs li a');
    if (tabs) {
        tabs.forEach(tab => {
            tab.addEventListener('click', function(e) {
                e.preventDefault();
                
                // Remove active class from all tabs and panes
                document.querySelectorAll('.tabs li').forEach(li => li.classList.remove('active'));
                document.querySelectorAll('.tab-pane').forEach(pane => pane.classList.remove('active'));
                
                // Add active class to current tab
                this.parentElement.classList.add('active');
                
                // Show the selected tab content
                const tabId = this.getAttribute('href');
                document.querySelector(tabId).classList.add('active');
            });
        });
    }
});