(function($){
   $(function(){
       $('.date').datepicker({
           dayNamesMin: ["Вс", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб"],
           firstDay: 1,
           dateFormat: "yy-mm-dd",
           changeMonth: true,
           changeYear: true
       });
   })
})(jQuery);
