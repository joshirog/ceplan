window.idleTimer = {
    init: function (dotNetHelper, idleSeconds) {
        let idleTime = 0;
        let countdownTimer;
        let warningShown = false;

        function resetTimer() {
            if (!warningShown) {
                idleTime = 0;
            }
        }

        function tick() {
            idleTime++;
            if (idleTime >= idleSeconds && !warningShown) {
                warningShown = true;
                // Llama al método C# ShowIdleWarning
                dotNetHelper.invokeMethodAsync('ShowIdleWarning');
                clearInterval(countdownTimer);
            }
        }

        // Reinicia el temporizador al detectar actividad
        ['mousemove','mousedown','keypress','touchstart'].forEach(evt =>
            document.addEventListener(evt, resetTimer)
        );

        // Cuenta cada segundo
        countdownTimer = setInterval(tick, 1000);
    }
};