// site.js - Basic functionality for Musical Instrument Shop

// Shopping cart functionality
const cart = {
    items: [],
    
    // Initialize cart from localStorage
    init: function() {
        const savedCart = localStorage.getItem('cart');
        if (savedCart) {
            this.items = JSON.parse(savedCart);
            this.updateCartCount();
        }
    },
    
    // Add item to cart
    addItem: function(productId, name, price, quantity = 1, imageUrl = '') {
        // Check if item already exists in cart
        const existingItem = this.items.find(item => item.productId === productId);
        
        if (existingItem) {
            // Update quantity if item already exists
            existingItem.quantity += quantity;
        } else {
            // Add new item to cart
            this.items.push({
                productId: productId,
                name: name,
                price: price,
                quantity: quantity,
                imageUrl: imageUrl
            });
        }
        
        // Save cart to localStorage
        this.saveCart();
        this.updateCartCount();
        
        return this.items.length;
    },
    
    // Remove item from cart
    removeItem: function(productId) {
        this.items = this.items.filter(item => item.productId !== productId);
        this.saveCart();
        this.updateCartCount();
    },
    
    // Update item quantity
    updateQuantity: function(productId, quantity) {
        const item = this.items.find(item => item.productId === productId);
        if (item) {
            item.quantity = quantity;
            this.saveCart();
            this.updateCartCount();
        }
    },
    
    // Get cart total
    getTotal: function() {
        return this.items.reduce((total, item) => total + (item.price * item.quantity), 0);
    },
    
    // Get total number of items in cart
    getItemCount: function() {
        return this.items.reduce((total, item) => total + item.quantity, 0);
    },
    
    // Save cart to localStorage
    saveCart: function() {
        localStorage.setItem('cart', JSON.stringify(this.items));
    },
    
    // Update cart count in header
    updateCartCount: function() {
        const cartCountElement = document.getElementById('cart-count');
        if (cartCountElement) {
            cartCountElement.textContent = this.getItemCount();
        }
    }
};

// Wait for the DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function() {
    // Initialize the cart
    cart.init();
    
    // Handle add to cart buttons
    const addToCartButtons = document.querySelectorAll('.add-to-cart');
    if (addToCartButtons) {
        addToCartButtons.forEach(button => {
            button.addEventListener('click', function() {
                const productId = parseInt(this.getAttribute('data-id'), 10);
                const productElement = this.closest('.product');
                
                if (productElement) {
                    const name = productElement.querySelector('h3').textContent;
                    const priceElement = productElement.querySelector('.price');
                    // Extract numeric price from ₹XX,XXX format
                    const priceText = priceElement.textContent.replace('₹', '').replace(/,/g, '');
                    const price = parseFloat(priceText);
                    
                    const imageElement = productElement.querySelector('img');
                    const imageUrl = imageElement ? imageElement.getAttribute('src') : '';
                    
                    const quantity = document.getElementById('quantity') 
                        ? parseInt(document.getElementById('quantity').value, 10) 
                        : 1;
                    
                    // Add item to cart
                    cart.addItem(productId, name, price, quantity, imageUrl);
                    
                    // Show success message
                    alert(`Added ${quantity} ${name} to cart!`);
                }
            });
        });
    }
    
    // Handle quantity changes on cart page
    const quantitySelects = document.querySelectorAll('.item-quantity select');
    if (quantitySelects) {
        quantitySelects.forEach(select => {
            select.addEventListener('change', function() {
                const cartItem = this.closest('.cart-item');
                if (cartItem) {
                    const productId = parseInt(cartItem.getAttribute('data-id'), 10);
                    const quantity = parseInt(this.value, 10);
                    
                    // Update quantity in cart
                    cart.updateQuantity(productId, quantity);
                    
                    // Update item total
                    const priceElement = cartItem.querySelector('.item-price');
                    const totalElement = cartItem.querySelector('.item-total');
                    
                    if (priceElement && totalElement) {
                        const priceText = priceElement.textContent.replace('₹', '').replace(/,/g, '');
                        const price = parseFloat(priceText);
                        const total = price * quantity;
                        
                        totalElement.textContent = `₹${total.toLocaleString('en-IN')}`;
                    }
                    
                    // Update cart summary
                    updateCartSummary();
                }
            });
        });
    }
    
    // Handle remove buttons on cart page
    const removeButtons = document.querySelectorAll('.remove-item');
    if (removeButtons) {
        removeButtons.forEach(button => {
            button.addEventListener('click', function() {
                const cartItem = this.closest('.cart-item');
                if (cartItem) {
                    const productId = parseInt(cartItem.getAttribute('data-id'), 10);
                    
                    // Remove item from cart
                    cart.removeItem(productId);
                    
                    // Remove item from DOM
                    cartItem.remove();
                    
                    // Update cart summary
                    updateCartSummary();
                    
                    // If cart is empty, show empty cart message
                    const cartItems = document.querySelectorAll('.cart-item');
                    if (cartItems.length === 0) {
                        const cartItemsContainer = document.querySelector('.cart-items');
                        if (cartItemsContainer) {
                            cartItemsContainer.innerHTML = '<div class="empty-cart">Your cart is empty. <a href="/Product">Continue shopping</a></div>';
                        }
                    }
                }
            });
        });
    }
    
    // Update cart summary on cart page
    function updateCartSummary() {
        const cartSubtotalElement = document.querySelector('.summary-row:nth-child(1) span:last-child');
        const cartTaxElement = document.querySelector('.summary-row:nth-child(3) span:last-child');
        const cartTotalElement = document.querySelector('.summary-row.total span:last-child');
        
        if (cartSubtotalElement && cartTaxElement && cartTotalElement) {
            const subtotal = cart.getTotal();
            const tax = subtotal * 0.18; // 18% GST
            const total = subtotal + tax + 500; // Adding fixed shipping of ₹500
            
            cartSubtotalElement.textContent = `₹${subtotal.toLocaleString('en-IN')}`;
            cartTaxElement.textContent = `₹${tax.toLocaleString('en-IN')}`;
            cartTotalElement.textContent = `₹${total.toLocaleString('en-IN')}`;
        }
    }
    
    // Product tabs functionality on product details page
    const tabs = document.querySelectorAll('.tabs li a');
    if (tabs && tabs.length > 0) {
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