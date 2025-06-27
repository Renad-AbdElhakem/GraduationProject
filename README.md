# Smart Flower Shop

**Smart Flower Shop** is an AI-powered e-commerce web application designed for flower enthusiasts and gardeners. Users can explore, identify, and purchase flowers and gardening products online, with image-based flower recognition and personalized care tips. Admins have full control over product and order management via a dedicated dashboard.

---

## Features

### User Features
- Register and log in to a personal account
- Upload flower images for species identification
- View care tips tailored to each flower
- Search and browse flowers with detailed descriptions
- Add products to cart and complete checkout
- Track orders and view order history

### Admin Features
- Add, update, or delete flower products
- View and manage customer orders
- Access a dashboard for full store control

---

## Screenshots

Place your screenshots in the `screenshots/` folder and update paths as needed.

| Homepage | Upload Image | Classification Result |
|---------|---------------|------------------------|
| ![Homepage](./screenshots/homepage.png) | ![Upload](./screenshots/image-upload.png) | ![Result](./screenshots/classification-result.png) |

| Shop | Cart & Checkout | Admin Panel |
|------|------------------|--------------|
| ![Shop](./screenshots/shop.png) | ![Cart](./screenshots/cart-checkout.png) | ![Admin](./screenshots/admin-panel.png) |

| Database ERD |
|------------------|
| ![DB](./screenshots/database-schema.png) |

---

## Demo

Watch a complete walkthrough of the project:  
[**View Demo**](https://drive.google.com/file/d/1INxlw0d-_BCsgVLcqQaHbli_1nYHJMH5/view?usp=drive_link)

---

## Technologies Used

- **Frontend:** HTML, CSS, JavaScript, Bootstrap  
- **Backend:** ASP.NET Core Web API (.NET ), Entity Framework Core, MySQL, ASP.NET Identity  
- **AI Component:** Python, FastAPI, OpenCV, YOLOv11x (used internally for classification)

---

## Flower Dataset & Model

- **Classes:** 100 flower species  
- **Images:** 15,880 total  
- **Testing Accuracy:** 98.38%  
- **Dataset:** [Download Dataset](https://drive.google.com/file/d/1ZXEmb14iU1RmhCMd84L6zKrl5eeR6YJR/view?usp=drive_link)  
- *(Internally trained using a custom classification model based on YOLOv11x)*

---

## Setup

To run the project locally:

### 1. Clone the Repository
```bash
  
git clone https://github.com/your-username/smart-flower-shop.git
