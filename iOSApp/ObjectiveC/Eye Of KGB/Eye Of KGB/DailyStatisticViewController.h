//
//  DailyStatisticViewController.h
//  Eye Of KGB
//
//  Created by bu on 23/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "SitePickerDailyStatisticViewController.h"
#import "PersonPickerDailyStatisticViewController.h"
#import "StartDatePickerViewController.h"
#import "EndDatePickerViewController.h"

@interface DailyStatisticViewController : UIViewController <SitePickerViewControllerDelegate, PersonPickerViewControllerDelegate, StartDatePickerViewControllerDelegate, EndDatePickerViewControllerDelegate>


@end
