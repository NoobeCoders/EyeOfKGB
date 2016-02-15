//
//  EndDatePickerViewController.h
//  Eye Of KGB
//
//  Created by bu on 08/02/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import <UIKit/UIKit.h>

@protocol EndDatePickerViewControllerDelegate <NSObject>
- (void)setEndDate:(NSString *)endDate andBoolForEndDateButtonName: (BOOL)boolForEndDateButtonName;
@end

@interface EndDatePickerViewController : UIViewController
@property (nonatomic, strong) id <EndDatePickerViewControllerDelegate> delegate;
@end
