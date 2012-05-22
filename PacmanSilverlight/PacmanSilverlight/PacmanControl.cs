using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PacmanSilverlight
{
    [TemplateVisualState(Name = "Normal")]
    [TemplateVisualState(Name = "MouseOver")]
    [TemplateVisualState(Name = "Pressed")]
    [TemplatePart(Name = TopRotator, Type = typeof(RotateTransform))]
    [TemplatePart(Name = BotRotator, Type = typeof(RotateTransform))]
    public class PacmanControl : Control
    {
        private const string BotRotator = "BotRotator";
        private const string TopRotator = "TopRotator";

        public static readonly DependencyProperty MouthAngleProperty =
            DependencyProperty.Register("MouthAngle", typeof(double), typeof(PacmanControl),
                new PropertyMetadata(default(double),
                    (o, args) => ((PacmanControl)o).PropertyChangedCallback()));

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(double), typeof(PacmanControl),
                new PropertyMetadata(default(double),
                    (o, args) => ((PacmanControl)o).PropertyChangedCallback()));

        private RotateTransform _botRotator;
        private RotateTransform _topRotator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PacmanControl"/> class.
        /// </summary>
        public PacmanControl()
        {
            DefaultStyleKey = typeof(PacmanControl);

            Loaded += (sender, args) => VisualStateManager.GoToState(this, "Normal", true);
            MouseEnter += (sender, args) => VisualStateManager.GoToState(this, "MouseOver", true);
            MouseLeave += (sender, args) => VisualStateManager.GoToState(this, "Normal", true);
            MouseLeftButtonDown += (sender, args) => VisualStateManager.GoToState(this, "Pressed", true);
            MouseLeftButtonUp += (sender, args) => VisualStateManager.GoToState(this, "Normal", true);
        }

        /// <summary>
        /// Gets or sets the pacman's mouth angle.
        /// </summary>
        /// <value>
        /// The pacman's mouth angle.
        /// </value>
        public double MouthAngle
        {
            get { return (double)GetValue(MouthAngleProperty); }
            set { SetValue(MouthAngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the pacman's size.
        /// </summary>
        /// <value>
        /// The pacman's size.
        /// </value>
        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>. In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _topRotator = GetTemplateChild(TopRotator) as RotateTransform;
            _botRotator = GetTemplateChild(BotRotator) as RotateTransform;
            PropertyChangedCallback();
        }

        private void PropertyChangedCallback()
        {
            if(_topRotator != null)
            {
                _topRotator.CenterX = Size / 2;
                _topRotator.CenterY = Size / 2;
                _topRotator.Angle = -MouthAngle;
            }
            if(_botRotator != null)
            {
                _botRotator.CenterX = Size / 2;
                _botRotator.Angle = MouthAngle;
            }
        }
    }
}