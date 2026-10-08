// =========================================================================
// YLG Lesson Practice & 2-Level Hint Management
// =========================================================================

function submitPractice(lessonId, exId, exType) {
    const box = document.getElementById(`practiceBox_${exId}`);
    if (!box) return;

    let attempts = parseInt(box.dataset.attempt || '0', 10) + 1;
    box.dataset.attempt = attempts;

    let answer = '';
    if (exType === 'single_choice') {
        const checked = document.querySelector(`input[name="opt_${exId}"]:checked`);
        if (!checked) {
            alert('Vui lòng chọn một phương án trả lời!');
            return;
        }
        answer = checked.value;
    } else {
        const input = document.getElementById(`input_${exId}`);
        if (!input || !input.value.trim()) {
            alert('Vui lòng nhập câu trả lời!');
            return;
        }
        answer = input.value.trim();
    }

    fetch('/Lesson/SubmitPractice', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            lessonId: lessonId,
            exerciseId: exId,
            userAnswer: answer,
            attemptCount: attempts
        })
    })
    .then(res => res.json())
    .then(data => {
        const feedback = document.getElementById(`feedback_${exId}`);
        const hint = document.getElementById(`hint_${exId}`);
        const expl = document.getElementById(`expl_${exId}`);

        if (feedback) {
            feedback.style.display = 'block';
            feedback.innerText = data.message;
            feedback.className = 'mt-2 small fw-bold ' + (data.isCorrect ? 'text-success' : 'text-danger');
        }

        if (data.isCorrect) {
            box.classList.add('correct');
            if (hint) hint.style.display = 'none';
            if (expl && data.explanation) {
                expl.style.display = 'block';
                expl.innerHTML = '<strong>Giải thích:</strong> ' + data.explanation;
            }
        } else {
            if (hint && data.hint) {
                hint.style.display = 'block';
                hint.innerHTML = '<i class="bi bi-lightbulb-fill me-1"></i> ' + data.hint;
            }
            if (expl && data.allowViewAnswer && data.correctAnswerText) {
                expl.style.display = 'block';
                expl.innerHTML = `<strong>Đáp án đúng:</strong> ${data.correctAnswerText}<br><strong>Giải thích:</strong> ${data.explanation || ''}`;
            }
        }
    })
    .catch(err => console.error('Error submitting practice:', err));
}
