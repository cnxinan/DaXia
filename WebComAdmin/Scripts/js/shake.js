//摇一摇部分
        var SHAKE_THRESHOLD = 1000;
        var last_update = 0;
        var last_time = 0;
        var x;
        var y;
        var z;
        var last_x;
        var last_y;
        var last_z;
        var sum = 0;
        var curTime;
        var isShakeble = true; 
        function init() {
            if (window.DeviceMotionEvent) {
                window.addEventListener('devicemotion', deviceMotionHandler, false);
            } else {
                $("#cantshake").show();
            }
        }

        var secs = 60;
        for(i=1;i<=secs;i++) {
            window.setTimeout("update(" + i + ")", i * 1000);
        }
        function update(num) {
            if(num == secs) {
                document.agree.agreeb.value ="已结束！";
                window.location.href = "/FrontProduct/Ranking";
                
            }
            else {
                printnr = secs-num;
                document.agree.agreeb.value = "剩余" + printnr +"秒";
            }
        }

        function deviceMotionHandler(eventData) {
            curTime = new Date().getTime();
            var diffTime = curTime - last_update;
            if (diffTime > 100) {
                var acceleration = eventData.accelerationIncludingGravity;
                last_update = curTime;
                x = acceleration.x;
                y = acceleration.y;
                z = acceleration.z;
                var speed = Math.abs(x + y + z - last_x - last_y - last_z) / diffTime * 10000;

                if (speed > SHAKE_THRESHOLD && curTime - last_time > 300 && isShakeble) {
                    shake();
                }
                last_x = x;
                last_y = y;
                last_z = z;
            }
        }

        function shake() {
            last_time = curTime;
            $("#shakeup").animate({ top: "10%" }, 100, function () {
                $("#shakeup").animate({ top: "21%" }, 100, function () {
                    sum++;
                    if (sum>0) {
                        $(".cSum").show();
                        $("#cSum").html(sum);
                    }else{
                        $(".cSum").hide();
                    }
                });
            });
            $("#shakedown").animate({ top: "40%" }, 100, function () {
                $("#shakedown").animate({ top: "21%" }, 100, function () {
                });
            });
           
        }
		
		//各种初始化
        $(document).ready(function () {
            Howler.iOSAutoEnable = false;
            FastClick.attach(document.body);
            init();
        });

        
		