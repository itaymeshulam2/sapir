import React, { useState } from "react";
import "./Booklet.css";
import axios from "axios";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function Booklet() {
  const [form, setForm] = useState({
    name: "",
    email: "",
    phone: "",
  });

  const [errors, setErrors] = useState({});
  const [serverError, setServerError] = useState(false);
  const [agree, setAgree] = useState(false);
  const [loading, setLoading] = useState(false);
  const [showModal, setShowModal] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
    setErrors({ ...errors, [name]: "" }); // Clear error when user types
  };

  const validateEmail = (email) => {
    // Simple email regex
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
  };

  const validatePhone = (phone) => {
    // Accepts numbers, and optional + at the start (you can customize this regex)
    const regex = /^[0-9]{7,15}$/;
    return regex.test(phone);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newErrors = {};

    if (!form.name.trim()) {
      newErrors.name = "שדה שם מלא הוא חובה";
    }

    if (!form.email.trim()) {
      newErrors.email = "שדה אימייל הוא חובה";
    } else if (!validateEmail(form.email)) {
      newErrors.email = "אימייל לא חוקי";
    }

    if (!form.phone.trim()) {
      newErrors.phone = "שדה טלפון הוא חובה";
    } else if (!validatePhone(form.phone)) {
      newErrors.phone = "מספר טלפון לא חוקי";
    }

    if (!agree) {
      newErrors.agree = "יש לאשר את קבלת המיילים";
    }

    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);
      return;
    }

    try {
      setLoading(true);
      const response = await axios.post(
        `${window.location.protocol}//${window.location.hostname}:7004/api/email/send-email`,
        {
          name: form.name,
          email: form.email,
          phone: form.phone,
        }
      );

      if (response.data.success) {
      
        setForm({ name: "", email: "", phone: "" });
        setAgree(false);
      } else {
        setServerError(true);
      }
    } catch (err) {
      setServerError(true);
      console.error("send email error:", err);
    }
    finally{
      setShowModal(true);
      setLoading(false);
    }
  };

  return (
    <div className="container">
      <img src="/text.jpeg" alt="Booklet" className="form-image" />

      <div className="bullets">
        <div className="bullets-header">מה תקבלי בה?</div>
        <div className="bullet">
          ✔️משחק חוויתי לכל המשפחה שדרכו ניתן לסכם את שנת הלימודים שהסתיימה -
          הכולל הוראות, לוח משחק, שחקנים, קובייה, קלפי שאלה וקלפי פקודה.
        </div>
        <div className="bullet">✔️לוח חופש לתכנון החופש הגדול.</div>
        <div className="bullet">✔️רעיונות לפעיליות ומקומות בילוי.</div>
        <div className="bullet">✔️כרטיסיות למילוי לוח החופש ביחד.</div>
      </div>

      <h2 style={{ margin: 'auto', marginTop: '30px' }}>
          החוברת להורדה ללא עלות!</h2>
      <div className="form-container">
        <div className="form-header">
          מלאי את הפרטים והחוברת תשלח אלייך ישירות למייל
        </div>

        <form onSubmit={handleSubmit}>
          <input
            placeholder="שם"
            type="text"
            name="name"
            value={form.name}
            onChange={handleChange}
          />
          {errors.name && <div className="error">{errors.name}</div>}

          <input
            placeholder="אימייל"
            type="email"
            name="email"
            value={form.email}
            onChange={handleChange}
          />
          {errors.email && <div className="error">{errors.email}</div>}

          <input
            dir="rtl"
            placeholder="טלפון"
            type="tel"
            name="phone"
            value={form.phone}
            onChange={handleChange}
          />
          {errors.phone && <div className="error">{errors.phone}</div>}

          <div className="checkbox-container">
            <label>
              <input
                type="checkbox"
                checked={agree}
                onChange={(e) => {
                  setAgree(e.target.checked);
                  setErrors({ ...errors, agree: "" });
                }}
              />
              אני מאשרת קבלת מיילים ועדכונים
            </label>
            {errors.agree && <div className="error">{errors.agree}</div>}
          </div>

          <button type="submit">שלחי לי את החוברת :)</button>
        </form>
      </div>
      {/* 🔥 Loader */}
      {loading && (
        <div className="loader-overlay">
          <div className="spinner"></div>
        </div>
      )}

      {/* 🎯 Modal */}
      {showModal && (
        <div className="modal-overlay">
        {/* <div className="modal-overlay"> */}
          <div className="modal-content">
            {serverError ?  <h2>אירע שגיאה אנא נסו בשנית</h2> : 
             <h2>החוברת נשלחה למייל שלך 🎉</h2>}
            <button onClick={() => setShowModal(false)}>סגור</button>
          </div>
        </div>   
      // </div>

      )}
    </div>
  );
}

export default Booklet;
