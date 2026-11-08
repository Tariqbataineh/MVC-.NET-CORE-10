Task 5 — فقط الجزء الأول والجزء الثاني من الملف
===============================================

الموجود:
1) Database First — SQL Server: CafeteriaDB
   - ملف Part1_DatabaseFirst_CafeteriaDB.sql ينشئ الجداول الخمسة والعلاقات.
   - Models وData/CafeteriaDbContext.cs مولدة بطريقة Database First من مشروعك.
2) Code First — SQL Server: CafeteriaEmployeesDB
   - CodeFirstModels للجداول الخمسة فقط.
   - Data/CodeFirstDbContext.cs
   - Migration جاهزة تحت Migrations/CodeFirst.

غير الموجود (لأنه ليس ضمن القسمين اللذين حددتهما):
- لا Login / Register أو Identity.
- لا Controllers لإدارة المنتجات أو Edit/Delete.
- لا Views أو نماذج إدخال.
- لا ViewModels أو Models إضافية خارج الجداول الخمسة في كل نهج.

طريقة الاستخدام:
1. افتح UniversityCafeteriaManagement.csproj باستخدام Visual Studio.
2. تأكد أن SQL Server هو DESKTOP-D59CRII، أو غيّر اسم السيرفر في appsettings.json.
3. Part 1: نفّذ ملف SQL في SSMS (إذا CafeteriaDB موجودة عندك مسبقًا، لا تعيد تنفيذ CREATE DATABASE والجداول).
4. Part 2: من Package Manager Console، بعد اختيار مشروع UniversityCafeteriaManagement:
   Update-Database -Context CodeFirstDbContext
5. تأكد من وجود 5 جداول في CafeteriaDB و5 جداول في CafeteriaEmployeesDB.

تنبيه: هذا المشروع خاص بإثبات إنشاء وربط قاعدتي البيانات حسب Part 1 وPart 2 فقط.
لن يعرض صفحات في المتصفح (404 متوقع)، لأنه لا يحتوي على Controllers أو Views حسب طلبك.
لا تنفّذ Update-Database على CafeteriaDbContext لأنه Database First.
