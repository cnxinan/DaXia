$(function () {
    UpdateClocks();
});
var timerID;
function UpdateClocks() {
    var ct = new Array(
        new tzone('北京: ', +8, 0, '#ffffff')
    );
    var dt = new Date();	// [GMT] time according to machine clock
    var startDST = new Date(dt.getFullYear(), 3, 1);
    while (startDST.getDay() != 0)
        startDST.setDate(startDST.getDate() + 1);
    var endDST = new Date(dt.getFullYear(), 9, 31);
    while (endDST.getDay() != 0)
        endDST.setDate(endDST.getDate() - 1);
    var ds_active;		// DS currently active
    if (startDST < dt && dt < endDST)
        ds_active = 1;
    else
        ds_active = 0;
    // Adjust each clock offset if that clock has DS and in DS.
    for (n = 0 ; n < ct.length ; n++)
        if (ct[n].ds == 1 && ds_active == 1) ct[n].os++;
    // compensate time zones
    gmdt = new Date();
    for (n = 0 ; n < ct.length ; n++)
        ct[n].ct = new Date(gmdt.getTime() + ct[n].os * 3600 * 1000);
    document.getElementById('Clock0').innerHTML = ClockString(ct[0].ct);
    //document.getElementById('Clock1').innerHTML = ClockString(ct[1].ct);
    timerID = window.setTimeout("UpdateClocks()", 1001);
}
function tzone(tz, os, ds, cl) {
    this.ct = new Date(0);		// datetime
    this.tz = tz;		// code
    this.os = os;		// GMT offset
    this.ds = ds;		// has daylight savings
    this.cl = cl;		// font color
}

function ClockString(dt) {
    var stemp, ampm;
    var dt_year = dt.getUTCFullYear();
    var dt_month = dt.getUTCMonth() + 1;
    var dt_day = dt.getUTCDate();
    var dt_hour = dt.getUTCHours();
    var dt_minute = dt.getUTCMinutes();
    var dt_second = dt.getUTCSeconds();

    if (dt_minute < 10) {
        dt_minute = '0' + dt_minute;
    }

    if (dt_second < 10) {
        dt_second = '0' + dt_second;
    }
    stemp = dt_hour + ":" + dt_minute + ":" + dt_second;
    return stemp;
}

function fillZero(v) {
    if (v < 10) { v = '0' + v; }
    return v;
}