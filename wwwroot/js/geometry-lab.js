// =========================================================================
// YLG Geometry Lab - Interactive SVG Geometry & Classification Engine
// =========================================================================

(function () {
    // Utility functions
    function dist(p1, p2) {
        return Math.hypot(p2.x - p1.x, p2.y - p1.y);
    }

    function angle(p1, pCenter, p2) {
        const a = dist(pCenter, p2);
        const b = dist(pCenter, p1);
        const c = dist(p1, p2);
        if (a === 0 || b === 0) return 0;
        const cosVal = (a * a + b * b - c * c) / (2 * a * b);
        return Math.round(Math.acos(Math.max(-1, Math.min(1, cosVal))) * 180 / Math.PI);
    }

    function getMousePos(svg, evt) {
        const rect = svg.getBoundingClientRect();
        const scaleX = svg.viewBox.baseVal.width / rect.width;
        const scaleY = svg.viewBox.baseVal.height / rect.height;
        return {
            x: (evt.clientX - rect.left) * scaleX,
            y: (evt.clientY - rect.top) * scaleY
        };
    }

    function classifyQuad(pts, dAB, dBC, dCD, dDA, dAC, dBD, angA, angB, angC, angD) {
        const isRightAngles = Math.abs(angA - 90) <= 6 && Math.abs(angB - 90) <= 6 && Math.abs(angC - 90) <= 6;
        const is4EqualSides = Math.abs(dAB - dBC) <= 8 && Math.abs(dBC - dCD) <= 8 && Math.abs(dCD - dDA) <= 8;
        const isOppSidesEqual = Math.abs(dAB - dCD) <= 8 && Math.abs(dBC - dDA) <= 8;
        const isDiagEqual = Math.abs(dAC - dBD) <= 8;

        if (isRightAngles && is4EqualSides) {
            return 'Hình vuông ⭐';
        } else if (isRightAngles) {
            return 'Hình chữ nhật';
        } else if (is4EqualSides) {
            return 'Hình thoi';
        } else if (isOppSidesEqual) {
            return 'Hình bình hành';
        } else if (isDiagEqual && Math.abs(pts.A.y - pts.B.y) <= 8 && Math.abs(pts.D.y - pts.C.y) <= 8) {
            return 'Hình thang cân';
        } else if (Math.abs(pts.A.y - pts.B.y) <= 8 && Math.abs(pts.D.y - pts.C.y) <= 8) {
            return 'Hình thang';
        }
        return 'Tứ giác thường';
    }

    // -------------------------------------------------------------------------
    // 1. STANDALONE GEOMETRY LAB (Views/GeometryLab/Index.cshtml)
    // -------------------------------------------------------------------------
    function initStandaloneLab() {
        const svg = document.getElementById('standaloneLabSvg');
        if (!svg) return;

        let activePoint = null;
        let isChallenge = svg.dataset.challenge === 'true';
        const targetChallengeShape = 'Hình thoi';
        let initialShapeName = svg.dataset.initialShape || 'HinhVuong';

        let pts = {
            A: { x: 150, y: 100 },
            B: { x: 450, y: 100 },
            C: { x: 450, y: 380 },
            D: { x: 150, y: 380 }
        };

        window.loadShape = function (shape) {
            if (shape === 'HinhVuong') {
                pts = { A: { x: 170, y: 110 }, B: { x: 430, y: 110 }, C: { x: 430, y: 370 }, D: { x: 170, y: 370 } };
            } else if (shape === 'HinhChuNhat') {
                pts = { A: { x: 130, y: 130 }, B: { x: 470, y: 130 }, C: { x: 470, y: 350 }, D: { x: 130, y: 350 } };
            } else if (shape === 'HinhThoi') {
                pts = { A: { x: 300, y: 80 }, B: { x: 480, y: 240 }, C: { x: 300, y: 400 }, D: { x: 120, y: 240 } };
            } else if (shape === 'HinhBinhHanh') {
                pts = { A: { x: 200, y: 120 }, B: { x: 500, y: 120 }, C: { x: 400, y: 360 }, D: { x: 100, y: 360 } };
            } else if (shape === 'HinhThangCan') {
                pts = { A: { x: 220, y: 120 }, B: { x: 380, y: 120 }, C: { x: 480, y: 360 }, D: { x: 120, y: 360 } };
            }
            updateLab();
        };

        function updateLab() {
            ['A', 'B', 'C', 'D'].forEach(k => {
                const c = document.getElementById('vert' + k);
                const l = document.getElementById('lblText' + k);
                if (c) { c.setAttribute('cx', pts[k].x); c.setAttribute('cy', pts[k].y); }
                if (l) { l.setAttribute('x', pts[k].x - 18); l.setAttribute('y', pts[k].y - 12); }
            });

            const poly = document.getElementById('polyShape');
            if (poly) poly.setAttribute('points', `${pts.A.x},${pts.A.y} ${pts.B.x},${pts.B.y} ${pts.C.x},${pts.C.y} ${pts.D.x},${pts.D.y}`);

            const dACLine = document.getElementById('diagAC');
            if (dACLine) {
                dACLine.setAttribute('x1', pts.A.x); dACLine.setAttribute('y1', pts.A.y);
                dACLine.setAttribute('x2', pts.C.x); dACLine.setAttribute('y2', pts.C.y);
            }

            const dBDLine = document.getElementById('diagBD');
            if (dBDLine) {
                dBDLine.setAttribute('x1', pts.B.x); dBDLine.setAttribute('y1', pts.B.y);
                dBDLine.setAttribute('x2', pts.D.x); dBDLine.setAttribute('y2', pts.D.y);
            }

            const dAB = Math.round(dist(pts.A, pts.B));
            const dBC = Math.round(dist(pts.B, pts.C));
            const dCD = Math.round(dist(pts.C, pts.D));
            const dDA = Math.round(dist(pts.D, pts.A));
            const dAC = Math.round(dist(pts.A, pts.C));
            const dBD = Math.round(dist(pts.B, pts.D));

            if (document.getElementById('valAB')) document.getElementById('valAB').innerText = dAB;
            if (document.getElementById('valBC')) document.getElementById('valBC').innerText = dBC;
            if (document.getElementById('valCD')) document.getElementById('valCD').innerText = dCD;
            if (document.getElementById('valDA')) document.getElementById('valDA').innerText = dDA;
            if (document.getElementById('valAC')) document.getElementById('valAC').innerText = dAC;
            if (document.getElementById('valBD')) document.getElementById('valBD').innerText = dBD;

            const angA = angle(pts.D, pts.A, pts.B);
            const angB = angle(pts.A, pts.B, pts.C);
            const angC = angle(pts.B, pts.C, pts.D);
            const angD = angle(pts.C, pts.D, pts.A);

            if (document.getElementById('valAngA')) document.getElementById('valAngA').innerText = angA + '°';
            if (document.getElementById('valAngB')) document.getElementById('valAngB').innerText = angB + '°';
            if (document.getElementById('valAngC')) document.getElementById('valAngC').innerText = angC + '°';
            if (document.getElementById('valAngD')) document.getElementById('valAngD').innerText = angD + '°';

            const shapeName = classifyQuad(pts, dAB, dBC, dCD, dDA, dAC, dBD, angA, angB, angC, angD);
            if (document.getElementById('currentShapeName')) {
                document.getElementById('currentShapeName').innerText = shapeName;
            }

            if (isChallenge) {
                const badge = document.getElementById('challengeStatusBadge');
                if (badge) {
                    if (shapeName.includes(targetChallengeShape)) {
                        badge.innerHTML = '<span class="badge bg-success px-3 py-2 fs-6">🎉 CHÍNH XÁC! THÀNH CÔNG!</span>';
                    } else {
                        badge.innerHTML = '<span class="badge bg-secondary px-3 py-2 fs-6">Đang thực hiện...</span>';
                    }
                }
            }
        }

        ['A', 'B', 'C', 'D'].forEach(k => {
            const el = document.getElementById('vert' + k);
            if (!el) return;
            el.addEventListener('mousedown', (e) => {
                activePoint = k;
                el.classList.add('active');
                e.preventDefault();
            });
        });

        window.addEventListener('mousemove', (e) => {
            if (!activePoint) return;
            const pos = getMousePos(svg, e);
            pts[activePoint].x = Math.max(30, Math.min(570, pos.x));
            pts[activePoint].y = Math.max(30, Math.min(450, pos.y));
            updateLab();
        });

        window.addEventListener('mouseup', () => {
            if (activePoint) {
                const el = document.getElementById('vert' + activePoint);
                if (el) el.classList.remove('active');
                activePoint = null;
            }
        });

        const resetBtn = document.getElementById('btnResetCanvas');
        if (resetBtn) {
            resetBtn.addEventListener('click', () => window.loadShape(initialShapeName));
        }

        const challengeBtn = document.getElementById('toggleChallengeBtn');
        if (challengeBtn) {
            challengeBtn.addEventListener('click', function () {
                isChallenge = !isChallenge;
                this.classList.toggle('active', isChallenge);
                const banner = document.getElementById('challengeBanner');
                if (banner) banner.style.display = isChallenge ? 'block' : 'none';
                updateLab();
            });
        }

        window.loadShape(initialShapeName);
    }

    // -------------------------------------------------------------------------
    // 2. LESSON EMBEDDED GEOMETRY LAB (Views/Lesson/Detail.cshtml)
    // -------------------------------------------------------------------------
    function initLessonLab() {
        const svg = document.getElementById('lessonLabSvg');
        if (!svg) return;

        let activePoint = null;
        const defaultShape = svg.dataset.defaultShape || 'HinhChuNhat';

        let pts = {
            A: { x: 120, y: 60 },
            B: { x: 380, y: 60 },
            C: { x: 380, y: 260 },
            D: { x: 120, y: 260 }
        };

        function resetPoints() {
            if (defaultShape === 'HinhVuong') {
                pts = { A: { x: 150, y: 70 }, B: { x: 350, y: 70 }, C: { x: 350, y: 270 }, D: { x: 150, y: 270 } };
            } else if (defaultShape === 'HinhThoi') {
                pts = { A: { x: 250, y: 50 }, B: { x: 380, y: 160 }, C: { x: 250, y: 270 }, D: { x: 120, y: 160 } };
            } else if (defaultShape === 'HinhBinhHanh') {
                pts = { A: { x: 160, y: 70 }, B: { x: 380, y: 70 }, C: { x: 320, y: 250 }, D: { x: 100, y: 250 } };
            } else if (defaultShape === 'HinhThangCan') {
                pts = { A: { x: 180, y: 70 }, B: { x: 320, y: 70 }, C: { x: 380, y: 250 }, D: { x: 120, y: 250 } };
            } else {
                pts = { A: { x: 120, y: 70 }, B: { x: 380, y: 70 }, C: { x: 380, y: 250 }, D: { x: 120, y: 250 } };
            }
            updateLessonLabUI();
        }

        function updateLessonLabUI() {
            ['A', 'B', 'C', 'D'].forEach(k => {
                const c = document.getElementById('pt' + k);
                const l = document.getElementById('lbl' + k);
                if (c) { c.setAttribute('cx', pts[k].x); c.setAttribute('cy', pts[k].y); }
                if (l) { l.setAttribute('x', pts[k].x - 15); l.setAttribute('y', pts[k].y - 10); }
            });

            const poly = document.getElementById('labPoly');
            if (poly) poly.setAttribute('points', `${pts.A.x},${pts.A.y} ${pts.B.x},${pts.B.y} ${pts.C.x},${pts.C.y} ${pts.D.x},${pts.D.y}`);

            const d1 = document.getElementById('labDiag1');
            const d2 = document.getElementById('labDiag2');
            if (d1) { d1.setAttribute('x1', pts.A.x); d1.setAttribute('y1', pts.A.y); d1.setAttribute('x2', pts.C.x); d1.setAttribute('y2', pts.C.y); }
            if (d2) { d2.setAttribute('x1', pts.B.x); d2.setAttribute('y1', pts.B.y); d2.setAttribute('x2', pts.D.x); d2.setAttribute('y2', pts.D.y); }

            const dAB = Math.round(dist(pts.A, pts.B));
            const dBC = Math.round(dist(pts.B, pts.C));
            const dCD = Math.round(dist(pts.C, pts.D));
            const dDA = Math.round(dist(pts.D, pts.A));
            const dAC = Math.round(dist(pts.A, pts.C));
            const dBD = Math.round(dist(pts.B, pts.D));

            if (document.getElementById('lenAB')) document.getElementById('lenAB').innerText = dAB;
            if (document.getElementById('lenBC')) document.getElementById('lenBC').innerText = dBC;
            if (document.getElementById('lenCD')) document.getElementById('lenCD').innerText = dCD;
            if (document.getElementById('lenDA')) document.getElementById('lenDA').innerText = dDA;

            const angA = angle(pts.D, pts.A, pts.B);
            const angB = angle(pts.A, pts.B, pts.C);
            const angC = angle(pts.B, pts.C, pts.D);
            const angD = angle(pts.C, pts.D, pts.A);

            if (document.getElementById('angA')) document.getElementById('angA').innerText = angA + '°';
            if (document.getElementById('angB')) document.getElementById('angB').innerText = angB + '°';
            if (document.getElementById('angC')) document.getElementById('angC').innerText = angC + '°';
            if (document.getElementById('angD')) document.getElementById('angD').innerText = angD + '°';

            const shapeName = classifyQuad(pts, dAB, dBC, dCD, dDA, dAC, dBD, angA, angB, angC, angD);
            if (document.getElementById('labDetectedShape')) {
                document.getElementById('labDetectedShape').innerText = shapeName;
            }
        }

        ['A', 'B', 'C', 'D'].forEach(k => {
            const el = document.getElementById('pt' + k);
            if (!el) return;
            el.addEventListener('mousedown', (e) => {
                activePoint = k;
                el.classList.add('active');
                e.preventDefault();
            });
        });

        window.addEventListener('mousemove', (e) => {
            if (!activePoint) return;
            const pos = getMousePos(svg, e);
            pts[activePoint].x = Math.max(30, Math.min(470, pos.x));
            pts[activePoint].y = Math.max(30, Math.min(310, pos.y));
            updateLessonLabUI();
        });

        window.addEventListener('mouseup', () => {
            if (activePoint) {
                const el = document.getElementById('pt' + activePoint);
                if (el) el.classList.remove('active');
                activePoint = null;
            }
        });

        const resetBtn = document.getElementById('btnResetLab');
        if (resetBtn) {
            resetBtn.addEventListener('click', resetPoints);
        }

        resetPoints();
    }

    // Auto-initialize when DOM is ready
    document.addEventListener('DOMContentLoaded', () => {
        initStandaloneLab();
        initLessonLab();
    });
})();
