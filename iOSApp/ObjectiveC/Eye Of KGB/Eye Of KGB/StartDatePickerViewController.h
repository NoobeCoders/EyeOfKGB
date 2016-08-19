//
//  StartDatePickerViewController.h
//  Eye Of KGB
//
//  Created by bu on 08/02/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import <UIKit/UIKit.h>

@protocol StartDatePickerViewControllerDelegate <NSObject>
- (void)setStartDate:(NSString *)startDate andBoolForStartDateButtonName: (BOOL)boolForStartDateButtonName;
@end

@interface StartDatePickerViewController : UIViewController

@property (nonatomic, strong) id <StartDatePickerViewControllerDelegate> delegate;

@end
