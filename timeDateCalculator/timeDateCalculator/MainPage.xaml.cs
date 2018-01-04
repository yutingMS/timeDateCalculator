using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace timeDateCalculator
{
    public partial class MainPage : ContentPage
    {
        void doClearAll()
        {
            startDateTime.Text = "";
            startDayName.Text = "dd";

            combndYears.Text = "0";
            combndMonths.Text = "0";
            combndWeeks.Text = "0";
            combndDays.Text = "0";
            combndHours.Text = "0";
            combndMinutes.Text = "0";

            totYears.Text = "0";
            totMonths.Text = "0";
            totWeeks.Text = "0";
            totDays.Text = "0";
            totHours.Text = "0";
            totMinutes.Text = "0";

            endDateTime.Text = "";
            endDayName.Text = "dd";

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    {
                        startDateTime.WidthRequest = 136;

                        combndYears.WidthRequest = 105;
                        combndMonths.WidthRequest = 105;
                        combndWeeks.WidthRequest = 105;
                        combndDays.WidthRequest = 105;
                        combndHours.WidthRequest = 105;
                        combndMinutes.WidthRequest = 105;

                        totYears.WidthRequest = 105;
                        totMonths.WidthRequest = 105;
                        totWeeks.WidthRequest = 105;
                        totDays.WidthRequest = 105;
                        totHours.WidthRequest = 105;
                        totMinutes.WidthRequest = 105;

                        endDateTime.WidthRequest = 136;

                        break;
                    }
                case Device.Android:
                    {
                        startDateTime.WidthRequest = 119;

                        combndYears.WidthRequest = 88;
                        combndMonths.WidthRequest = 88;
                        combndWeeks.WidthRequest = 88;
                        combndDays.WidthRequest = 88;
                        combndHours.WidthRequest = 88;
                        combndMinutes.WidthRequest = 88;

                        totYears.WidthRequest = 88;
                        totMonths.WidthRequest = 88;
                        totWeeks.WidthRequest = 88;
                        totDays.WidthRequest = 88;
                        totHours.WidthRequest = 88;
                        totMinutes.WidthRequest = 88;

                        endDateTime.WidthRequest = 119;

                        break;
                    }
                case Device.UWP:
                    {
                        startDateTime.WidthRequest = 165;

                        combndYears.WidthRequest = 121;
                        combndMonths.WidthRequest = 121;
                        combndWeeks.WidthRequest = 121;
                        combndDays.WidthRequest = 121;
                        combndHours.WidthRequest = 121;
                        combndMinutes.WidthRequest = 121;

                        totYears.WidthRequest = 121;
                        totMonths.WidthRequest = 121;
                        totWeeks.WidthRequest = 121;
                        totDays.WidthRequest = 121;
                        totHours.WidthRequest = 121;
                        totMinutes.WidthRequest = 121;

                        endDateTime.WidthRequest = 165;

                        break;
                    }
                default: //Set as UWP
                    {
                        startDateTime.WidthRequest = 165;

                        combndYears.WidthRequest = 121;
                        combndMonths.WidthRequest = 121;
                        combndWeeks.WidthRequest = 121;
                        combndDays.WidthRequest = 121;
                        combndHours.WidthRequest = 121;
                        combndMinutes.WidthRequest = 121;

                        totYears.WidthRequest = 121;
                        totMonths.WidthRequest = 121;
                        totWeeks.WidthRequest = 121;
                        totDays.WidthRequest = 121;
                        totHours.WidthRequest = 121;
                        totMinutes.WidthRequest = 121;

                        endDateTime.WidthRequest = 165;

                        break;
                    }
            }

        }

        private double width;
        private double height;
        private bool firstTime = true;

        private const double iPhone6SPlusPortraitWidth = 414;
        private const double iPhone6SPlusPortraitHeight = 736;

        private const double iPhone6SPlusLandscapeWidth = iPhone6SPlusPortraitHeight;
        private const double iPhone6SPlusPLandscapeHeight = iPhone6SPlusPortraitWidth;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            if (firstTime)
            {
                doClearAll();
                firstTime = false;
            }

            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                double widthAndHightScale;

                if (height > width) // Portrait ?
                { // Portrait
                    //startDateTimeStack.Orientation = StackOrientation.Horizontal;
                    entriesOuterStack.Orientation = StackOrientation.Horizontal;
                    combinedTimeEntriesStack.Orientation = StackOrientation.Vertical;
                    totalTimeEntriesStack.Orientation = StackOrientation.Vertical;

                    startDayName.WidthRequest = 2.0f * Math.Truncate(startDayName.Width);
                    endDayName.WidthRequest = 2.0f * Math.Truncate(endDayName.Width);

                    var widthScale = width / iPhone6SPlusPortraitWidth;
                    var heightScale = height / iPhone6SPlusPortraitHeight;
                    widthAndHightScale = (widthScale <= heightScale) ? widthScale : heightScale;
                }
                else
                { // Landscape
                    //startDateTimeStack.Orientation = StackOrientation.Vertical;
                    entriesOuterStack.Orientation = StackOrientation.Vertical;
                    combinedTimeEntriesStack.Orientation = StackOrientation.Horizontal;
                    totalTimeEntriesStack.Orientation = StackOrientation.Horizontal;

                    var widthScale = width / iPhone6SPlusLandscapeWidth;
                    var heightScale = height / iPhone6SPlusPLandscapeHeight;
                    widthAndHightScale = (widthScale <= heightScale) ? widthScale : heightScale;
                }

                if ((widthAndHightScale < 1.0) || (widthAndHightScale > 1.0))
                {
                    IList<Setter> baseEntryStyleNameSetters = baseEntryStyleName.Setters;
                    baseEntryStyleName.Setters[0].Value = (double)(baseEntryStyleName.Setters[0].Value) * widthAndHightScale;
                    baseLabelStyleName.Setters[0].Value = (double)(baseLabelStyleName.Setters[0].Value) * widthAndHightScale;
                }
            }
        }

        string FormatDateTime(string theDateTimeStringToFormat, ref string dayName)
        { // yyyyMMddHHmm -> yyyy-MM-dd HH:mm
            var theFormattedDateTime = theDateTimeStringToFormat;

            DateTime dateTimeHolder = DateTime.Now;
            try
            {
                dateTimeHolder = DateTime.Parse(theFormattedDateTime);
            }
            catch (FormatException)
            {
                if (theFormattedDateTime.Length < 12)
                {
                    for (int i = theFormattedDateTime.Length + 1; i <= 12; i++)
                    {
                        theFormattedDateTime += "0";
                    }
                }
                if (theFormattedDateTime.Length > 12)
                {
                    theFormattedDateTime = theFormattedDateTime.Remove(12);
                }

                theFormattedDateTime = theFormattedDateTime.Insert(10, ":");
                theFormattedDateTime = theFormattedDateTime.Insert(8, " ");
                theFormattedDateTime = theFormattedDateTime.Insert(6, "-");
                theFormattedDateTime = theFormattedDateTime.Insert(4, "-");

                try
                {
                    dateTimeHolder = DateTime.Parse(theFormattedDateTime);
                }
                catch (FormatException)
                {
                    DisplayAlert("Illegal Date+Time !", "", "OK");
                    theFormattedDateTime = "";
                }
            }
            dayName = dateTimeHolder.ToString("R").Remove(3);
            return theFormattedDateTime;

        }


        bool startDateTimeJustFocused = false;
        bool startDateTimeChanged = false;
        string startDateTimeContentOnFocused = "";

        void FormatStartDateTime()
        {
            if (startDateTime.Text.Length != 0)
            {
                var dayName = "";
                startDateTime.Text = FormatDateTime(startDateTime.Text, ref dayName);
                startDayName.Text = dayName;
            }
        }

        void OnStartDateTimeNowButtonClicked(object sender, EventArgs args)
        { // yyyy-MM-dd HH:mm
            startDateTimeJustFocused = false;
            startDateTimeChanged = false;
            startDateTime.Text = DateTime.Now.ToString("u").Remove(16);
            startDayName.Text = DateTime.Now.ToString("R").Remove(3);
        }

        void OnStartDateTimeFocused(object sender, EventArgs args)
        {
            startDateTimeContentOnFocused = startDateTime.Text;
            startDateTime.Text = "";
            startDayName.Text = "dd";
            startDateTimeJustFocused = true;
            startDateTimeChanged = false;
        }

        void OnStartDateTimeUnfocused(object sender, EventArgs args)
        { // yyyyMMddHHmm -> yyy-MM-dd HH:mm
            startDateTimeJustFocused = false;

            if (startDateTimeChanged)
            {
                startDateTimeChanged = false;
                FormatStartDateTime();
            }
            else
            {
                startDateTime.Text = startDateTimeContentOnFocused;
                FormatStartDateTime();
            }
        }

        void OnStartDateTimeTextChanged(object sender, EventArgs args)
        {
            if (startDateTimeJustFocused)
            {
                startDateTimeJustFocused = false;
                startDateTimeChanged = true;
            }
        }

        void OnStartDateTimeCompleted(object sender, EventArgs args)
        { // yyyyMMddHHmm -> yyy-MM-dd HH:mm
            FormatStartDateTime();
        }


        bool endDateTimeJustFocused = false;
        bool endDateTimeChanged = false;
        string endDateTimeContentOnFocused = "";

        void FormatEndDateTime()
        {
            if (endDateTime.Text.Length != 0)
            {
                var dayName = "";
                endDateTime.Text = FormatDateTime(endDateTime.Text, ref dayName);
                endDayName.Text = dayName;
            }
        }

        void OnEndDateTimeNowButtonClicked(object sender, EventArgs args)
        { // yyyy-MM-dd HH:mm
            endDateTimeJustFocused = false;
            endDateTimeChanged = false;
            endDateTime.Text = DateTime.Now.ToString("u").Remove(16);
            endDayName.Text = DateTime.Now.ToString("R").Remove(3);
        }

        void OnEndDateTimeFocused(object sender, EventArgs args)
        {
            endDateTimeContentOnFocused = endDateTime.Text;
            endDateTime.Text = "";
            endDayName.Text = "dd";
            endDateTimeJustFocused = true;
            endDateTimeChanged = false;
        }

        void OnEndDateTimeUnfocused(object sender, EventArgs args)
        { // yyyyMMddHHmm -> yyyy-MM-dd HH:mm
            endDateTimeJustFocused = false;

            if (endDateTimeChanged)
            {
                endDateTimeChanged = false;
                FormatEndDateTime();
            }
            else
            {
                endDateTime.Text = endDateTimeContentOnFocused;
                FormatEndDateTime();
            }
        }

        void OnEndDateTimeTextChanged(object sender, EventArgs args)
        {
            if (endDateTimeJustFocused)
            {
                endDateTimeJustFocused = false;
                endDateTimeChanged = true;
            }
        }

        void OnEndDateTimeCompleted(object sender, EventArgs args)
        { // yyyyMMddHHmm -> yyyy-MM-dd HH:mm
            FormatEndDateTime();
        }

        void OnCalculateButtonClicked(object sender, EventArgs args)
        {
            Task<bool> task = DisplayAlert("CalculateButtonClicked", "Decide on an option",
                                           "yes or ok", "no or cancel");
        }

        void OnClearAllButtonClicked(object sender, EventArgs args)
        {
            doClearAll();
            //Task<bool> task = DisplayAlert("ClearAllButtonClicked", "Decide on an option",
            //                               "yes or ok", "no or cancel");
        }
    }
}
