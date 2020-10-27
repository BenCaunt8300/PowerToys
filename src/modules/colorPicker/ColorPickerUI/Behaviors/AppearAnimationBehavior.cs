﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;
using ColorPicker.Constants;

namespace ColorPicker.Behaviors
{
    public class AppearAnimationBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            Appear();
            AssociatedObject.IsVisibleChanged += AssociatedObject_IsVisibleChanged;
        }

        private void AssociatedObject_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (AssociatedObject.IsVisible)
            {
                Appear();
            }
            else
            {
                Hide();
            }
        }

        private void Appear()
        {
            var duration = new Duration(TimeSpan.FromMilliseconds(250));

            var opacityAppear = new DoubleAnimation(0d, 1d, duration)
            {
                EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut },
            };

            var resize = new DoubleAnimation(0d, WindowConstant.PickerWindowWidth, duration)
            {
                 EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut },
            };

            AssociatedObject.BeginAnimation(UIElement.OpacityProperty, opacityAppear);
            AssociatedObject.BeginAnimation(FrameworkElement.WidthProperty, resize);
        }

        private void Hide()
        {
            var duration = new Duration(TimeSpan.FromMilliseconds(1));

            var opacityAppear = new DoubleAnimation(0d, duration);
            var resize = new DoubleAnimation(0d, duration);

            AssociatedObject.BeginAnimation(UIElement.OpacityProperty, opacityAppear);
            AssociatedObject.BeginAnimation(FrameworkElement.WidthProperty, resize);
        }
    }
}
